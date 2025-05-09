using Microsoft.EntityFrameworkCore;
using StoreManager.Application.Product.Blueprint.Repository;
using StoreManager.Domain.Product.Blueprint.Model;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Product.Blueprint.Repository;

public class ProductBlueprintBlueprintRepository : IProductBlueprintRepository
{
    private readonly DbSet<ProductBlueprint> _blueprints;

    public ProductBlueprintBlueprintRepository(WarehouseDbContext context)
    {
        _blueprints = context.ProductBlueprints;
    }

    public Task<ProductBlueprint?> FindByIdAsync(Guid id)
    {
        return _blueprints
            .Include(p => p.Exports)
            .ThenInclude(ei => ei.Export)
            .ThenInclude(e => e.Exporter)
            .Include(p => p.Components)
            .ThenInclude(mc => mc.Component)
            .FirstOrDefaultAsync(p => p.Id.Equals(id));
    }

    public async Task<ProductBlueprint> CreateAsync(ProductBlueprint product)
    {
        var savedInstance = await _blueprints.AddAsync(product);
        return savedInstance.Entity;
    }

    public async Task<(ICollection<ProductBlueprint> Items, int TotalCount)> FindFilteredAsync(string? productInfo,
        DateOnly? dateCreated, int pageNumber, int pageSize, ISpecification<ProductBlueprint> spec)
    {
        var query = spec.Apply(_blueprints.AsQueryable());

        if (!string.IsNullOrEmpty(productInfo))
        {
            query = query.Where(p =>
                p.Identifier.ToLower().Contains(productInfo.ToLower()) ||
                p.Name.ToLower().Contains(productInfo.ToLower()) ||
                p.Description.ToLower().Contains(productInfo.ToLower()));
        }

        if (dateCreated.HasValue)
        {
            query = query.Where(p => p.DateCreated.Equals(dateCreated));
        }

        var skip = (pageNumber - 1) * pageSize;
        var count = await query.CountAsync();
        query = query.Skip(skip).Take(pageSize).AsNoTracking();

        return (await query.ToListAsync(), count);
    }

    public Task<List<ProductBlueprint>> FindByInvoiceIdAsync(Guid invoiceId)
    {
        var products = _blueprints
            .Include(p => p.Exports)
            .Where(p => p.Exports
                .Any(e => e.ExportId.Equals(invoiceId)));
        return products.ToListAsync();
    }

    public Task<List<ProductBlueprint>> FindByExporterIdAsync(Guid exporterId)
    {
        var query = _blueprints
            .Include(p => p.Exports)
            .ThenInclude(e => e.Export)
            .Where(p=>p.Exports.Any(e=>e.Export.ExporterId.Equals(exporterId)));
        return query.ToListAsync();
    }
}