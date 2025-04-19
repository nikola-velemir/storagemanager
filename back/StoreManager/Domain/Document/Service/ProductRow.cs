namespace StoreManager.Domain.Document.Service;

public class ProductRow
{
    public string Name { get; set; } = string.Empty;
    public string Identifier { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
    public double Price { get; set; } = 0;
}