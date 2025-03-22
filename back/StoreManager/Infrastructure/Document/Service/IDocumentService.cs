using StoreManager.Infrastructure.Document.DTO;

namespace StoreManager.Infrastructure.Document.Service
{
    public interface IDocumentService
    {
        Task<DocumentDownloadResponseDTO> DownloadFile(string fileName);

        Task<DocumentDownloadResponseDTO> DownloadDocumentChunk(string fileName);
        Task UploadChunk(IFormFile file, string fileName, int chunkIndex, int totalChunks);

        Task UploadFile(IFormFile file);
    }
}
