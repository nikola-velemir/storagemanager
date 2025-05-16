using StoreManager.Domain.Product.Blueprint.Model;
using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Domain.MechanicalComponent.Model
{
    public class MechanicalComponent
    {
        public required Guid Id { get; set; }
        public required string Identifier { get; set; }
        public string Name { get; set; } = string.Empty;
        public required int CurrentStock { get; set; }
        public ICollection<ImportItemModel> Items { get; set; } = new List<ImportItemModel>();
        public ICollection<ProductBlueprintLineItem> Products { get; set; } = new List<ProductBlueprintLineItem>();

        public void IncreaseStock(int value) => CurrentStock += value;
        
        public void DecreaseStock(int value) => CurrentStock -= value;

    }
}
