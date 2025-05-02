using StoreManager.Domain.Product.Blueprint.Model;

namespace StoreManager.Domain.Product.Batch.Model;

public class ProductBatch
{
    public Guid Id { get; set; }
    public required Guid BlueprintId { get; set; }
    public required ProductBlueprint  Blueprint { get; set; }
    public int Quantity { get; set; }
    public DateOnly CreatedOn { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
}