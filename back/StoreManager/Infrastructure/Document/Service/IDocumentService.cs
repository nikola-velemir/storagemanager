using StoreManager.Infrastructure.Document.DTO;

namespace StoreManager.Infrastructure.Document.Service
{
    public interface IDocumentService
    {
        Task DownloadFile(HttpResponse response, CancellationToken cancellationToken, string fileName);
        Task<RequestDocumentDownloadResponseDTO> RequestDownload(string fileName);
        Task<DocumentDownloadResponseDTO> DownloadChunk(string fileName, int chunkIndex);

        Task<DocumentDownloadResponseDTO> DownloadDocumentChunk(string fileName);
        Task UploadChunk(IFormFile file, string fileName, int chunkIndex, int totalChunks);

        Task UploadFile(IFormFile file);
    }
}
