using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Product.Model;

namespace StoreManager.Infrastructure.MechanicalComponent.Model
{
    public class MechanicalComponentModel
    {
        public required Guid Id { get; set; }
        public required string Identifier { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<ImportItemModel> Items { get; set; } = new List<ImportItemModel>();
        public ICollection<ProductComponentsModel> Products { get; set; } = new List<ProductComponentsModel>();
    }
}
