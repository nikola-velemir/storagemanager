using StoreManager.Infrastructure.MechanicalComponent.Model;

namespace StoreManager.Infrastructure.Product.Model;

public class ProductComponentsModel
{
    public required MechanicalComponentModel Component { get; set; }
    public required ProductModel Product { get; set; }
    public required Guid ComponentId { get; set; }
    public required Guid ProductId { get; set; }
    public int UsedQuantity { get; set; }
}