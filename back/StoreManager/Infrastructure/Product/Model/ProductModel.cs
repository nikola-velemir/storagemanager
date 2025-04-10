namespace StoreManager.Infrastructure.Product.Model;

public class ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<ProductComponentsModel> Components { get; set; } = new List<ProductComponentsModel>();
}