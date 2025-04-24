using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Domain.Product.Model;

public class ProductBlueprint
{
    public required Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public required string Identifier { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateOnly DateCreated { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public ICollection<ProductBlueprintLineItems> Components { get; set; } = new List<ProductBlueprintLineItems>();
    
    public ICollection<ExportItemModel> Exports { get; set; } = new List<ExportItemModel>();
}