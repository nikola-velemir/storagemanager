using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Document.Service
{
    public interface IDocumentService
    {
        Task<RequestDocumentDownloadResponseDTO> RequestDownload(string fileName);
        Task<DocumentDownloadResponseDTO> DownloadChunk(string fileName, int chunkIndex);
        Task AppendChunk(IFormFile file, DocumentModel foundFile);

        Task UploadChunk(string provider, IFormFile file, string fileName, int chunkIndex, int totalChunks);
        Task DeleteAllChunks(DocumentModel file);
    }
}
