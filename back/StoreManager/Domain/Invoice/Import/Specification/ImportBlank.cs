using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Domain.Invoice.Import.Specification;

public class ImportBlank : ISpecification<Model.Import>
{
    public IQueryable<Model.Import> Apply(IQueryable<Model.Import> query)
    {
        return query;
    }
}