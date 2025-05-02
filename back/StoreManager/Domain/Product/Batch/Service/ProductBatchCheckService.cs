using StoreManager.Domain.Common;
using StoreManager.Domain.Product.Batch.Errors;
using StoreManager.Domain.Product.Blueprint.Model;

namespace StoreManager.Domain.Product.Batch.Service;

public class ProductBatchCheckService : IProductBatchCheckService
{
    public Result CreateBatch(ProductBlueprint blueprint, int productQuantity)
    {
        var lineItems = blueprint.Components.ToList();
        
        foreach (var lineItem in lineItems)
        {
            var requiredQuantity = lineItem.UsedQuantity * productQuantity;
            
            var component = lineItem.Component;
            var currentQuantity =component.CurrentStock;
            if (requiredQuantity > currentQuantity)
                return ProductBatchErrors.StockLimitExceeded;
            component.DecreaseStock(requiredQuantity);
        }

        return Result.Success();

    }
}