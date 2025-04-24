using StoreManager.Domain.Invoice.Export.Model;
using StoreManager.Domain.Product.Model;

namespace StoreManager.Infrastructure.Invoice.Export.Model;

public class ExportItemModel
{
    
    public required Domain.Invoice.Export.Model.Export Export { get; set; }
    public required ProductBlueprint Product { get; set; }
    public required Guid ExportId { get; set; }
    public required Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public double PricePerPiece { get; set; }

}