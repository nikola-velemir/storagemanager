using System.Text.RegularExpressions;
using MediatR;
using StoreManager.Application.BusinessPartner.Provider.Repository;
using StoreManager.Application.Common;
using StoreManager.Application.Document.Command;
using StoreManager.Application.Document.Repository;
using StoreManager.Application.Document.Service.FileService;
using StoreManager.Application.Document.Service.Reader;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Domain;
using StoreManager.Domain.Document.Specification;
using StoreManager.Domain.Document.Storage.Service;
using StoreManager.Domain.Invoice.Import.Service;
using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.Document.Handler
{
    public class UploadChunkHandler(
        IUnitOfWork unitOfWork,
        IProviderRepository providerRepository,
        IDocumentRepository documentRepository,
        IImportRepository importRepository,
        ICloudStorageService supaService,
        IMechanicalComponentRepository mechanicalComponentRepository,
        IFileService fileService,
        IDocumentReaderFactory readerFactory,
        IWebHostEnvironment env,
        IImportService importService)
        : IRequestHandler<UploadChunkCommand,Result>
    {
        public async Task<Result> Handle(UploadChunkCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ValidateRequest(request);

                var providerId = Guid.Parse(request.ProviderId);
                var provider = await providerRepository.FindByIdAsync(providerId);

                var parsedFileName =
                    Regex.Replace(Path.GetFileNameWithoutExtension(request.FileName), @"[^a-zA-Z0-9]", "");
                var foundDocument =
                    await documentRepository.FindByNameAsync(new DocumentWithDocumentChunks(), parsedFileName);
                Import? import = null;
                if (foundDocument == null)
                {
                    foundDocument = await documentRepository.SaveFileAsync(request.FileName);
                    import = await importRepository.Create(new Import
                    {
                        Provider = provider,
                        ProviderId = provider.Id,
                        Type = InvoiceType.Import,
                        DateIssued = DateOnly.FromDateTime(DateTime.UtcNow),
                        Document = foundDocument,
                        DocumentId = foundDocument.Id,
                        Id = Guid.NewGuid()
                    });
                    provider.AddImport(import);
                }
                else
                {
                    import = await importRepository.FindByDocumentId(foundDocument.Id);
                }

                var savedChunk = await documentRepository.SaveChunkAsync(foundDocument, request.File, request.FileName,
                    request.ChunkIndex);

                await supaService.UploadFileChunk(request.File, savedChunk);

                await fileService.AppendChunk(request.File, foundDocument);

                if (request.ChunkIndex != request.TotalChunks - 1) return Result.Success();

                var documentReader = readerFactory.GetReader(DocumentUtils.GetRawMimeType(foundDocument.Type));

                var webRootPath = Path.Combine(env.WebRootPath, "uploads", "invoice");

                var filePath = Path.Combine(webRootPath,
                    $"{foundDocument.Id.ToString()}.{DocumentUtils.GetRawMimeType(foundDocument.Type)}");

                var metadata = documentReader.ExtractDataFromDocument(filePath);
                 await mechanicalComponentRepository.CreateFromExtractionMetadataAsync((metadata));
              
                await importService.Create(import, metadata);

                await fileService.DeleteAllChunks(foundDocument);

                await unitOfWork.CommitAsync(cancellationToken);

                return Result.Success();
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