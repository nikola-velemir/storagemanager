using Microsoft.EntityFrameworkCore;
using StoreManager.Domain.Product.Batch;
using StoreManager.Domain.Product.Batch.Model;
using StoreManager.Domain.Product.Batch.Repository;
using StoreManager.Infrastructure.Context;

namespace StoreManager.Infrastructure.Product.Batch.Repository;

public class ProductBatchRepository : IProductBatchRepository
{
    private readonly DbSet<ProductBatch> _productBatches;

    public ProductBatchRepository(WarehouseDbContext context)
    {
        _productBatches = context.ProductBatches;
    }

    public async Task<ProductBatch> CreateAsync(ProductBatch productBatch)
    {
        var savedInstance = await _productBatches.AddAsync(productBatch);
        return savedInstance.Entity;
    }
}