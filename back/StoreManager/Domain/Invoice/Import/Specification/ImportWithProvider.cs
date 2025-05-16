using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Domain.Invoice.Import.Specification;

public class ImportWithProvider : ISpecification<Model.Import>
{
    public IQueryable<Model.Import> Apply(IQueryable<Model.Import> query)
    {
        return query.Include(i => i.Document);
    }
}