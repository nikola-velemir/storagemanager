using StoreManager.Domain.Product.Batch.Model;

namespace StoreManager.Domain.Product.Batch.Repository;

public interface IProductBatchRepository
{
    Task<ProductBatch> CreateAsync(ProductBatch productBatch);
}