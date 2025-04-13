using MediatR;
using Newtonsoft.Json;
using StoreManager.Infrastructure.Document.Command;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.Service.FileService;
using StoreManager.Infrastructure.Document.Service.Reader;
using StoreManager.Infrastructure.Document.SupaBase.Service;
using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.Invoice.Service;
using StoreManager.Infrastructure.Provider.DTO;
using StoreManager.Infrastructure.Provider.Model;
using StoreManager.Infrastructure.Provider.Repository;
using System.Text.RegularExpressions;

namespace StoreManager.Infrastructure.Document.Handler
{
    public class UploadChunkHandler(
        IProviderRepository providerRepository,
        IDocumentRepository documentRepository,
        IInvoiceRepository invoiceRepository,
        ICloudStorageService supaService,
        IFileService fileService,
        IDocumentReaderFactory readerFactory,
        IWebHostEnvironment env,
        IInvoiceService invoiceService)
        : IRequestHandler<UploadChunkCommand>
    {
        public async Task<Unit> Handle(UploadChunkCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var parsedProvider = JsonConvert.DeserializeObject<ProviderFormDataRequestDto>(request.ProviderFormData);
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
                        Adress = parsedProvider.ProviderAddress,
                        Id = Guid.NewGuid(),
                        Name = parsedProvider.ProviderName,
                        PhoneNumber = parsedProvider.ProviderPhoneNumber
                    });
                }
                var parsedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(request.FileName), @"[^a-zA-Z0-9]", "");
                var foundFile = await documentRepository.FindByName(parsedFileName);
                if (foundFile == null)
                {
                    foundFile = await documentRepository.SaveFile(request.FileName);
                    var invoice = await invoiceRepository.Create(new InvoiceModel
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

                    var filePath = Path.Combine(webRootPath, $"{foundFile.Id.ToString()}.{DocumentUtils.GetRawMimeType(foundFile.Type)}");

                    var metadata = documentReader.ExtractDataFromDocument(filePath);

                    await invoiceService.Create(foundFile.Id, metadata);

                    await fileService.DeleteAllChunks(foundFile);
                }

                return Unit.Value;
            }
            catch (Exception)
            {
                throw new BadHttpRequestException("Failed to upload file!");
            }
        }
    }
}
