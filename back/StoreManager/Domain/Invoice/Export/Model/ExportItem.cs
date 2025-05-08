using StoreManager.Domain.Product.Blueprint.Model;

namespace StoreManager.Domain.Invoice.Export.Model;

public class ExportItem
{
    
    public required Export Export { get; set; }
    public required ProductBlueprint Product { get; set; }
    public required Guid ExportId { get; set; }
    public required Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public double PricePerPiece { get; set; }

}