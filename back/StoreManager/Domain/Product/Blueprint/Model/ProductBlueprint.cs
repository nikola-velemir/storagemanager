using StoreManager.Domain.Invoice.Export.Model;
using StoreManager.Domain.Product.Batch;
using StoreManager.Domain.Product.Batch.Model;

namespace StoreManager.Domain.Product.Blueprint.Model;

public class ProductBlueprint
{
    public required Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public required string Identifier { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateOnly DateCreated { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public ICollection<ProductBlueprintLineItems> Components { get; set; } = new List<ProductBlueprintLineItems>();
    public ICollection<ProductBatch> Batches { get; set; } = new List<ProductBatch>();
    public ICollection<ExportItem> Exports { get; set; } = new List<ExportItem>();
}