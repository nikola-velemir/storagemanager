using Microsoft.EntityFrameworkCore;
using StoreManager.Domain.Document.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Domain.Document.Specification;

public class DocumentWithDocumentChunks : ISpecification<Model.Document>
{
    public IQueryable<Model.Document> Apply(IQueryable<Model.Document> query)
    {
        return query.Include(d => d.Chunks);
    }
}