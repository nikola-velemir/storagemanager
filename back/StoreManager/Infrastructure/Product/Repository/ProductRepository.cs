using DocumentFormat.OpenXml.Office2019.Presentation;
using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Product.Model;

namespace StoreManager.Infrastructure.Product.Repository;

public class ProductRepository(WarehouseDbContext context) : IProductRepository
{
    private readonly DbSet<ProductModel> _products = context.Products;


    public Task<ProductModel?> FindById(Guid id)
    {
        return _products
            .Include(p=>p.Exports)
                .ThenInclude(ei=>ei.Export)
                .ThenInclude(e=>e.Exporter)
            .Include(p=>p.Components)
                .ThenInclude(mc=>mc.Component)
            .FirstOrDefaultAsync(p => p.Id.Equals(id));
    }

    public async Task<ProductModel> Create(ProductModel product)
    {
        var savedInstance = await _products.AddAsync(product);
        await context.SaveChangesAsync();
        return savedInstance.Entity;
    }

    public async Task<(ICollection<ProductModel> Items, int TotalCount)> FindFiltered(string? productInfo,
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
}