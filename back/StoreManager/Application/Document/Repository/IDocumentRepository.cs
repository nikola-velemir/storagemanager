using StoreManager.Domain.Document.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Document.Repository;
    public interface IDocumentRepository
    {
        Task<Domain.Document.Model.Document?> FindByNameAsync(ISpecification<Domain.Document.Model.Document> spec, string fileName);
        Task<Domain.Document.Model.Document> SaveFileAsync(string fileName);

        Task<DocumentChunk> SaveChunkAsync(Domain.Document.Model.Document foundDoc, IFormFile? file, string fileName,
            int chunkIndex);
    }
