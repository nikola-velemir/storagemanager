using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Document.Command;
using StoreManager.Application.Document.DTO;
using StoreManager.Application.Document.Errors;
using StoreManager.Application.Document.Repository;
using StoreManager.Domain.Document.Specification;
using StoreManager.Domain.Document.Storage.Service;
using StoreManager.Domain.Invoice.Base.Repository;

namespace StoreManager.Application.Document.Handler
{
    public class DownloadChunkHandler(
        IInvoiceRepository importRepository,
        ICloudStorageService cloudStorageService,
        IDocumentRepository documentRepository)
        : IRequestHandler<DownloadChunkQuery, Result<DocumentDownloadResponseDto>>
    {
        public async Task<Result<DocumentDownloadResponseDto>> Handle(DownloadChunkQuery request,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.InvoiceId, out var tempId))
            {
                return DocumentErrors.DocumentIdParseError;
            }

            var invoice = await importRepository.FindById(tempId);
            if (invoice is null)
            {
                return DocumentErrors.DocumentNotFound;
            }

            var file = await documentRepository.FindByNameAsync(new DocumentWithDocumentChunks(),
                invoice.Document.FileName);
            if (file is null)
                return DocumentErrors.DocumentNotFound;


            var chunk = file.Chunks.FirstOrDefault(chunk => chunk.ChunkNumber == request.ChunkIndex);
            if (chunk is null)
                return DocumentErrors.DocumentChunkNotFound;


            var response = new DocumentDownloadResponseDto(await cloudStorageService.DownloadChunk(chunk),
                DocumentUtils.GetPresentationalMimeType(file.Type));
            return Result.Success(response);
        }
    }
}