using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Domain.Invoice.Import.Specification;

public class ImportBlank : ISpecification<ImportModel>
{
    public IQueryable<ImportModel> Apply(IQueryable<ImportModel> query)
    {
        return query;
    }
}