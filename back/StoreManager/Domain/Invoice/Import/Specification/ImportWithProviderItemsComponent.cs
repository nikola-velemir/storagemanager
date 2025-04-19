using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Import.Repository.Specification;

public class ImportWithProviderItemsComponent : ISpecification<ImportModel>
{
    public IQueryable<ImportModel> Apply(IQueryable<ImportModel> query)
    {
        return query.Include(invoice => invoice.Provider)
            .Include(invoice => invoice.Items)
            .ThenInclude(item => item.Component);
    }
}