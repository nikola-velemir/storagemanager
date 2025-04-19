using StoreManager.Domain.Document.Model;

namespace StoreManager.Domain.Document.Storage.Service
{
    public interface ICloudStorageService
    {
        public Task<string> UploadFileChunk(IFormFile fileChunk, DocumentChunkModel chunk);

        public Task<byte[]> DownloadChunk(DocumentChunkModel chunk);
    }
}
