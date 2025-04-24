using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Import.Repository.Specification;

public class ImportWithDocument : ISpecification<Model.Import>
{
    public IQueryable<Model.Import> Apply(IQueryable<Model.Import> query)
    {
       return query.Include(e=>e.Document);
    }
}