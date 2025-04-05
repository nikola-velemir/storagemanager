using StoreManager.Infrastructure.Invoice.Model;

namespace StoreManager.Infrastructure.MechanicalComponent.Model
{
    public class MechanicalComponentModel
    {
        public required Guid Id { get; set; }
        public required string Identifier { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<InvoiceItemModel> Items { get; set; } = new List<InvoiceItemModel>();
    }
}
