using MediatR;
using StoreManager.Infrastructure.Document.DTO;

namespace StoreManager.Infrastructure.Document.Command
{
    public record DownloadChunkQuery(string InvoiceId, int ChunkIndex) : IRequest<DocumentDownloadResponseDTO>;
}
