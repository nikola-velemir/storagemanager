using StoreManager.Domain.Common;

namespace StoreManager.Domain.Product.Batch.Errors;

public static class ProductBatchErrors
{
    public static DomainError StockLimitExceeded => new DomainError("Stock Limit Exceeded","One components stock has been exceeded.");
}