using Microsoft.EntityFrameworkCore;
using StoreManager.Application.Product.Repository;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Product.Model;

namespace StoreManager.Infrastructure.Product.Repository;

public class ProductRepository(WarehouseDbContext context) : IProductRepository
{
    private readonly DbSet<ProductModel> _products = context.Products;

    public Task<ProductModel?> FindByIdAsync(Guid id)
    {
        return _products
            .Include(p => p.Exports)
            .ThenInclude(ei => ei.Export)
            .ThenInclude(e => e.Exporter)
            .Include(p => p.Components)
            .ThenInclude(mc => mc.Component)
            .FirstOrDefaultAsync(p => p.Id.Equals(id));
    }

    public async Task<ProductModel> CreateAsync(ProductModel product)
    {
        var savedInstance = await _products.AddAsync(product);
        await context.SaveChangesAsync();
        return savedInstance.Entity;
    }

    public async Task<(ICollection<ProductModel> Items, int TotalCount)> FindFilteredAsync(string? productInfo,
        DateOnly? dateCreated, int pageNumber, int pageSize)
    {
        var query = _products.Include(p => p.Components).AsQueryable();

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
        query = query.Skip(skip).Take(pageSize);

        return (await query.ToListAsync(), count);
    }

    public Task<List<ProductModel>> FindByInvoiceIdAsync(Guid invoiceId)
    {
        var products = _products
            .Include(p => p.Exports)
            .Where(p => p.Exports
                .Any(e => e.ExportId.Equals(invoiceId)));
        return products.ToListAsync();
    }

    public Task<List<ProductModel>> FindByExporterIdAsync(Guid exporterId)
    {
        var query = _products
            .Include(p => p.Exports)
            .ThenInclude(e => e.Export)
            .Where(p=>p.Exports.Any(e=>e.Export.ExporterId.Equals(exporterId)));
        return query.ToListAsync();
    }
}