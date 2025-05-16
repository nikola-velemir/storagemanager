using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Import.Repository.Specification;

public class ImportWithDocument : ISpecification<Domain.Invoice.Import.Model.Import>
{
    public IQueryable<Domain.Invoice.Import.Model.Import> Apply(IQueryable<Domain.Invoice.Import.Model.Import> query)
    {
       return query.Include(e=>e.Document);
    }
}