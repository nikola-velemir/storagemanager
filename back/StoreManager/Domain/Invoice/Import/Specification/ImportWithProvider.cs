using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Domain.Invoice.Import.Specification;

public class ImportWithProvider : ISpecification<Infrastructure.Invoice.Import.Model.Import>
{
    public IQueryable<Infrastructure.Invoice.Import.Model.Import> Apply(IQueryable<Infrastructure.Invoice.Import.Model.Import> query)
    {
        return query.Include(i => i.Document);
    }
}