using Microsoft.EntityFrameworkCore;
using StoreManager.Domain.Invoice.Export.Model;
using StoreManager.Infrastructure.Invoice.Export.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Domain.Invoice.Export.Specification;

public class FindFilteredExportsSpecification : ISpecification<Model.Export>
{
    public IQueryable<Model.Export> Apply(IQueryable<Model.Export> query)
    {
        return query
            .Include(e => e.Document)
            .Include(e => e.Exporter)
            .Include(e => e.Items)
            .ThenInclude(i => i.Product)
            .AsQueryable();
    }
}