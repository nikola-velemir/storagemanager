using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Document.Repository
{
    public interface IDocumentRepository
    {
        Task<DocumentModel?> FindByName(string fileName);
        Task<DocumentModel> SaveFile(string fileName);

        Task<DocumentChunkModel> SaveChunk(IFormFile file, string fileName, int chunkIndex);

    }
}
