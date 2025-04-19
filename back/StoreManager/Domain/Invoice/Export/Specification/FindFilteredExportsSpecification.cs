using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Invoice.Export.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Domain.Invoice.Export.Specification;

public class FindFilteredExportsSpecification : ISpecification<ExportModel>
{
    public IQueryable<ExportModel> Apply(IQueryable<ExportModel> query)
    {
        return query
            .Include(e => e.Document)
            .Include(e => e.Exporter)
            .Include(e => e.Items)
            .ThenInclude(i => i.Product)
            .AsQueryable();
    }
}