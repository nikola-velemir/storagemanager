using Microsoft.EntityFrameworkCore;
using StoreManager.Domain.Product.Blueprint.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Domain.Product.Blueprint.Specification;

public class ProductBlueprintsWithBatchesSpecification : ISpecification<ProductBlueprint>
{
    public IQueryable<ProductBlueprint> Apply(IQueryable<ProductBlueprint> query)
    {
        return query
            .Include(p=>p.Batches)
            .AsQueryable();
    }
}