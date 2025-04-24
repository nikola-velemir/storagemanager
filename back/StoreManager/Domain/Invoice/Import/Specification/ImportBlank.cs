using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Domain.Invoice.Import.Specification;

public class ImportBlank : ISpecification<Infrastructure.Invoice.Import.Model.Import>
{
    public IQueryable<Infrastructure.Invoice.Import.Model.Import> Apply(IQueryable<Infrastructure.Invoice.Import.Model.Import> query)
    {
        return query;
    }
}