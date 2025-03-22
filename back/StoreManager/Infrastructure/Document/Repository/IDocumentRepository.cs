using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Document.Repository
{
    public interface IDocumentRepository
    {
        Task<DocumentModel?> FindByName(string fileName);
        Task<DocumentModel> SaveFile(string fileName);

        //  Task<DocumentModel> SaveFile(IFormFile file);
        Task<DocumentChunkModel> SaveChunk(IFormFile file, string fileName, int chunkIndex);
        bool AreAllChunksReceived(string fileName, int totalChunks);
        Task<FileInfo> MergeChunks(string fileName, int totalChunks);
    }
}
