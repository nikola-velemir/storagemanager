using StoreManager.Domain.Document.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Document.Repository
{
    public interface IDocumentRepository
    {
        Task<DocumentModel?> FindByNameAsync(ISpecification<DocumentModel> spec, string fileName);
        Task<DocumentModel> SaveFileAsync(string fileName);

        Task<DocumentChunkModel> SaveChunkAsync(IFormFile file, string fileName, int chunkIndex);

    }
}
