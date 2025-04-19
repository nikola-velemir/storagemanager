using Microsoft.EntityFrameworkCore;
using StoreManager.Domain.Document.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Domain.Document.Specification;

public class DocumentWithDocumentChunks : ISpecification<DocumentModel>
{
    public IQueryable<DocumentModel> Apply(IQueryable<DocumentModel> query)
    {
        return query.Include(d => d.Chunks);
    }
}