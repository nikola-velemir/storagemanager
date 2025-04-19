using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.Product.Model;

public class ProductModel
{
    public required Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public required string Identifier { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateOnly DateCreated { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public ICollection<ProductComponentsModel> Components { get; set; } = new List<ProductComponentsModel>();
    
    public ICollection<ExportItemModel> Exports { get; set; } = new List<ExportItemModel>();
}