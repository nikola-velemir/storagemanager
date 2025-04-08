using DocumentFormat.OpenXml.Bibliography;
using MediatR;

namespace StoreManager.Infrastructure.Document.Command
{
    public record UploadChunkCommand(string ProviderFormData, IFormFile File, string FileName, int ChunkIndex, int TotalChunks) :IRequest;
}
