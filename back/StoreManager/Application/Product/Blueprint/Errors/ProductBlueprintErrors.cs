using StoreManager.Application.Common;
using StoreManager.Application.Product.Blueprint.Command;

namespace StoreManager.Application.Product.Blueprint.Errors;

public static class ProductBlueprintErrors
{
    public static Error ProductBlueprintNotFound =>
        new Error("ProductBlueprintNotFound", 404, "ProductBlueprintNotFound");
}