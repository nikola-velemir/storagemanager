using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Document.Service
{
    public interface IDocumentService
    {
        Task<DocumentDownloadResponseDTO> DownloadChunk(string fileName, int chunkIndex);

        Task UploadChunk(string provider, IFormFile file, string fileName, int chunkIndex, int totalChunks);
    }
}
