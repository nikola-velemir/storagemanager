using StoreManager.Infrastructure.MechanicalComponent.Model;

namespace StoreManager.Infrastructure.Invoice.Model
{
    public class InvoiceItemModel
    {
        public required InvoiceModel Invoice { get; set; }
        public required MechanicalComponentModel Component { get; set; }
        public required int Quantity { get; set; }

        public required Guid InvoiceId { get; set; }
        public required Guid ComponentId { get; set; }
    }
}
