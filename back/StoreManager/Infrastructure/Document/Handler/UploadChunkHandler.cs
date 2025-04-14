using MediatR;
using Newtonsoft.Json;
using StoreManager.Infrastructure.Document.Command;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.Service.FileService;
using StoreManager.Infrastructure.Document.Service.Reader;
using StoreManager.Infrastructure.Document.SupaBase.Service;
using System.Text.RegularExpressions;
using StoreManager.Infrastructure.BusinessPartner.Base;
using StoreManager.Infrastructure.BusinessPartner.Provider.DTO;
using StoreManager.Infrastructure.BusinessPartner.Provider.Model;
using StoreManager.Infrastructure.BusinessPartner.Provider.Repository;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Invoice.Import.Repository;
using StoreManager.Infrastructure.Invoice.Import.Service;
using StoreManager.Infrastructure.MiddleWare.Exceptions;
using StoreManager.Infrastructure.Product.Command;

namespace StoreManager.Infrastructure.Document.Handler
{
    public class UploadChunkHandler(
        IProviderRepository providerRepository,
        IDocumentRepository documentRepository,
        IImportRepository importRepository,
        ICloudStorageService supaService,
        IFileService fileService,
        IDocumentReaderFactory readerFactory,
        IWebHostEnvironment env,
        IImportService importService)
        : IRequestHandler<UploadChunkCommand>
    {
        public async Task<Unit> Handle(UploadChunkCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ValidateRequest(request);
                var parsedProvider =
                    JsonConvert.DeserializeObject<ProviderFormDataRequestDto>(request.ProviderFormData);
                if (parsedProvider is null)
                {
                    throw new ArgumentNullException("provider is null");
                }

                ProviderModel? provider;
                if (!string.IsNullOrEmpty(parsedProvider.ProviderId))
                {
                    provider = await providerRepository.FindById(Guid.Parse(parsedProvider.ProviderId));
                    if (provider is null) throw new ArgumentNullException("provider is null");
                }
                else
                {
                    provider = await providerRepository.Create(new ProviderModel
                    {
                        Address = parsedProvider.ProviderAddress,
                        Id = Guid.NewGuid(),
                        Name = parsedProvider.ProviderName,
                        Type = BusinessPartnerType.Provider,
                        PhoneNumber = parsedProvider.ProviderPhoneNumber
                    });
                }

                var parsedFileName =
                    Regex.Replace(Path.GetFileNameWithoutExtension(request.FileName), @"[^a-zA-Z0-9]", "");
                var foundFile = await documentRepository.FindByName(parsedFileName);
                if (foundFile == null)
                {
                    foundFile = await documentRepository.SaveFile(request.FileName);
                    var invoice = await importRepository.Create(new ImportModel
                    {
                        Provider = provider,
                        ProviderId = provider.Id,
                        DateIssued = DateOnly.FromDateTime(DateTime.UtcNow),
                        Document = foundFile,
                        DocumentId = foundFile.Id,
                        Id = Guid.NewGuid()
                    });
                    await providerRepository.AddInvoice(provider, invoice);
                }

                var savedChunk = await documentRepository.SaveChunk(request.File, request.FileName, request.ChunkIndex);
                await supaService.UploadFileChunk(request.File, savedChunk);

                await fileService.AppendChunk(request.File, foundFile);

                if (request.ChunkIndex == request.TotalChunks - 1)
                {
                    var documentReader = readerFactory.GetReader(DocumentUtils.GetRawMimeType(foundFile.Type));

                    var webRootPath = Path.Combine(env.WebRootPath, "uploads", "invoice");

                    var filePath = Path.Combine(webRootPath,
                        $"{foundFile.Id.ToString()}.{DocumentUtils.GetRawMimeType(foundFile.Type)}");

                    var metadata = documentReader.ExtractDataFromDocument(filePath);

                    await importService.Create(foundFile.Id, metadata);

                    await fileService.DeleteAllChunks(foundFile);
                }

                return Unit.Value;
            }
            catch (Exception)
            {
                throw new BadHttpRequestException("Failed to upload file!");
            }
        }

        private static void ValidateRequest(UploadChunkCommand request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.ProviderFormData))
                errors.Add("Provider form data is empty!");

            if (string.IsNullOrWhiteSpace(request.FileName))
                errors.Add("FileName is required.");
            else if (request.ChunkIndex > request.TotalChunks)
                errors.Add("Chunk index is out of range!");

            if (request.File.Length <=0)
                errors.Add("File length is required!");

            if (errors.Count != 0)
                throw new ValidationException(string.Join(" ", errors));
        }
    }
}