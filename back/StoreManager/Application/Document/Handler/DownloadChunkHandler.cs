using MediatR;
using StoreManager.Application.Document.Command;
using StoreManager.Application.Document.DTO;
using StoreManager.Application.Document.Repository;
using StoreManager.Domain.Document.Specification;
using StoreManager.Domain.Document.Storage.Service;
using StoreManager.Infrastructure.Invoice.Base.Repository;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.Document.Handler
{
    public class DownloadChunkHandler(
        IInvoiceRepository importRepository,
        ICloudStorageService cloudStorageService,
        IDocumentRepository documentRepository)
        : IRequestHandler<DownloadChunkQuery, DocumentDownloadResponseDto>
    {
        public async Task<DocumentDownloadResponseDto> Handle(DownloadChunkQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.InvoiceId, out var tempId))
            {
                throw new InvalidCastException("Guid cannot be parsed");
            }
            var invoiceGuid = Guid.Parse(request.InvoiceId);
            var invoice = await importRepository.FindById(invoiceGuid);
            if (invoice is null)
            {
                throw new NotFoundException("Invoice not found");
            }

            var file = await documentRepository.FindByName(new DocumentWithDocumentChunks(), invoice.Document.FileName) ?? throw new NotFoundException("File not found");



            var chunk = file.Chunks.FirstOrDefault(chunk => chunk.ChunkNumber == request.ChunkIndex)
                ?? throw new NotFoundException("Chunk not found");

            var response = new DocumentDownloadResponseDto(await cloudStorageService.DownloadChunk(chunk), DocumentUtils.GetPresentationalMimeType(file.Type));
            return response;
        }
    }
}
