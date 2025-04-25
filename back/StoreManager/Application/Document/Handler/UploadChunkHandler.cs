using System.Text.RegularExpressions;
using MediatR;
using Newtonsoft.Json;
using StoreManager.Application.BusinessPartner.Provider.DTO;
using StoreManager.Application.BusinessPartner.Provider.Repository;
using StoreManager.Application.Document.Command;
using StoreManager.Application.Document.Repository;
using StoreManager.Application.Document.Service.FileService;
using StoreManager.Application.Document.Service.Reader;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Domain;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Domain.Document.Specification;
using StoreManager.Domain.Document.Storage.Service;
using StoreManager.Infrastructure;
using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Invoice.Import.Service;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.Document.Handler
{
    public class UploadChunkHandler(
        IUnitOfWork unitOfWork,
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

                var providerId = Guid.Parse(request.ProviderId);
                var provider = await providerRepository.FindByIdAsync(providerId);
                
                var parsedFileName =
                    Regex.Replace(Path.GetFileNameWithoutExtension(request.FileName), @"[^a-zA-Z0-9]", "");
                var foundFile = await documentRepository.FindByNameAsync(new DocumentWithDocumentChunks(), parsedFileName);
                if (foundFile == null)
                {
                    foundFile = await documentRepository.SaveFileAsync(request.FileName);
                    var invoice = await importRepository.Create(new Import
                    {
                        Provider = provider,
                        ProviderId = provider.Id,
                        Type = InvoiceType.Import,
                        DateIssued = DateOnly.FromDateTime(DateTime.UtcNow),
                        Document = foundFile,
                        DocumentId = foundFile.Id,
                        Id = Guid.NewGuid()
                    });
                    await providerRepository.AddInvoiceAsync(provider, invoice);
                }

                var savedChunk = await documentRepository.SaveChunkAsync(request.File, request.FileName, request.ChunkIndex);
                await supaService.UploadFileChunk(request.File, savedChunk);

                await fileService.AppendChunk(request.File, foundFile);

                if (request.ChunkIndex != request.TotalChunks - 1) return Unit.Value;
                
                var documentReader = readerFactory.GetReader(DocumentUtils.GetRawMimeType(foundFile.Type));

                var webRootPath = Path.Combine(env.WebRootPath, "uploads", "invoice");

                var filePath = Path.Combine(webRootPath,
                    $"{foundFile.Id.ToString()}.{DocumentUtils.GetRawMimeType(foundFile.Type)}");

                var metadata = documentReader.ExtractDataFromDocument(filePath);

                await importService.Create(foundFile.Id, metadata);

                await fileService.DeleteAllChunks(foundFile);
                
                await unitOfWork.SaveChangesAsync(cancellationToken);
                
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

            if (string.IsNullOrWhiteSpace(request.ProviderId))
                errors.Add("Provider form data is empty!");
            else if (!Guid.TryParse(request.ProviderId, out _))
                errors.Add("Provider id is invalid!");

            if (string.IsNullOrWhiteSpace(request.FileName))
                errors.Add("FileName is required.");
            else if (request.ChunkIndex > request.TotalChunks)
                errors.Add("Chunk index is out of range!");

            if (request.File.Length <= 0)
                errors.Add("File length is required!");

            if (errors.Count != 0)
                throw new ValidationException(string.Join(" ", errors));
        }
    }
}