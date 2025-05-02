namespace StoreManager.Domain.Product.Blueprint.Model;

public class ProductBlueprintLineItems
{
    public required MechanicalComponent.Model.MechanicalComponent Component { get; set; }
    public required ProductBlueprint Product { get; set; }
    public required Guid ComponentId { get; set; }
    public required Guid ProductId { get; set; }
    public int UsedQuantity { get; set; }
}