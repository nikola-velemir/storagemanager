using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Document.SupaBase.Service
{
    public interface ICloudStorageService
    {
        public Task<string> UploadFileChunk(IFormFile fileChunk, DocumentChunkModel chunk);

        public Task<byte[]> DownloadChunk(DocumentChunkModel chunk);
    }
}
