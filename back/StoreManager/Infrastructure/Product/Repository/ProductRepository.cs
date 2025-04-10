using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Product.Model;

namespace StoreManager.Infrastructure.Product.Repository;

public class ProductRepository(WarehouseDbContext context) : IProductRepository
{
    private readonly DbSet<ProductModel> _products = context.Products;


    public Task<ProductModel?> FindById(Guid id)
    {
        return _products.FirstOrDefaultAsync(p => p.Id.Equals(id));
    }

    public async Task<ProductModel> Create(ProductModel product)
    {
        var savedInstance = await _products.AddAsync(product);
        await context.SaveChangesAsync();
        return savedInstance.Entity;
    }
}