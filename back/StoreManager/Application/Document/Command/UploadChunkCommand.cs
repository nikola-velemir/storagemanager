using MediatR;

namespace StoreManager.Application.Document.Command
{
    public record UploadChunkCommand(
        string ProviderId,
        IFormFile File,
        string FileName,
        int ChunkIndex,
        int TotalChunks) : IRequest;
}