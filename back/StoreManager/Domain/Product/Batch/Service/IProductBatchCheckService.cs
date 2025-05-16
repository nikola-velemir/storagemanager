using StoreManager.Domain.Common;
using StoreManager.Domain.Product.Blueprint.Model;

namespace StoreManager.Domain.Product.Batch.Service;

public interface IProductBatchCheckService
{
    Result CreateBatch(ProductBlueprint blueprint, int productQuantity);
}