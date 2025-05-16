using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Import.Repository.Specification;

public class ImportWithProviderItemsComponent : ISpecification<Domain.Invoice.Import.Model.Import>
{
    public IQueryable<Domain.Invoice.Import.Model.Import> Apply(IQueryable<Domain.Invoice.Import.Model.Import> query)
    {
        return query.Include(invoice => invoice.Provider)
            .Include(invoice => invoice.Items)
            .ThenInclude(item => item.Component);
    }
}