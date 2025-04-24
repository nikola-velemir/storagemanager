using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Import.Repository.Specification;

public class ImportWithProviderItemsComponent : ISpecification<Model.Import>
{
    public IQueryable<Model.Import> Apply(IQueryable<Model.Import> query)
    {
        return query.Include(invoice => invoice.Provider)
            .Include(invoice => invoice.Items)
            .ThenInclude(item => item.Component);
    }
}