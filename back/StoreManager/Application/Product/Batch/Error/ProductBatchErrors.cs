namespace StoreManager.Application.Product.Batch.Error;

public static class ProductBatchErrors
{
    public static Common.Error StockLimitExceeded => new Common.Error("StockLimitExceeded",400,"One components stock has been exceeded.");
}