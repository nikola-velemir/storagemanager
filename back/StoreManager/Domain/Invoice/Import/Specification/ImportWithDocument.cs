using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Import.Repository.Specification;

public class ImportWithDocument : ISpecification<ImportModel>
{
    public IQueryable<ImportModel> Apply(IQueryable<ImportModel> query)
    {
       return query.Include(e=>e.Document);
    }
}