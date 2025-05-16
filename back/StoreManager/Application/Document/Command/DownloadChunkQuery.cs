using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Document.DTO;

namespace StoreManager.Application.Document.Command
{
    public record DownloadChunkQuery(string InvoiceId, int ChunkIndex) : IRequest<Result<DocumentDownloadResponseDto>>;
}