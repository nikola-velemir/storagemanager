using StoreManager.Domain.Invoice.Export.Model;
using StoreManager.Infrastructure.Product.Model;

namespace StoreManager.Infrastructure.Invoice.Export.Model;

public class ExportItemModel
{
    
    public required ExportModel Export { get; set; }
    public required ProductModel Product { get; set; }
    public required Guid ExportId { get; set; }
    public required Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public double PricePerPiece { get; set; }

}