using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Document.Service
{
    public interface IDocumentService
    {
        Task<RequestDocumentDownloadResponseDTO> RequestDownload(string fileName);
        Task<DocumentDownloadResponseDTO> DownloadChunk(string fileName, int chunkIndex);
        Task UploadChunk(IFormFile file, string fileName, int chunkIndex, int totalChunks); public Task LoadAndSaveFile(string fileName);

    }
}
