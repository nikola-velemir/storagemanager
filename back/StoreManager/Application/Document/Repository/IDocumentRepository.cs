using StoreManager.Domain.Document.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Document.Repository
{
    public interface IDocumentRepository
    {
        Task<DocumentModel?> FindByName(ISpecification<DocumentModel> spec, string fileName);
        Task<DocumentModel> SaveFile(string fileName);

        Task<DocumentChunkModel> SaveChunk(IFormFile file, string fileName, int chunkIndex);

    }
}
