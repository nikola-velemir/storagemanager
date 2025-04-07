using StoreManager.Infrastructure.Invoice.Model;

namespace StoreManager.Infrastructure.Provider.Model
{
    public class ProviderModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Adress { get; set; }
        public required string PhoneNumber { get; set; }
        public ICollection<InvoiceModel> Invoices { get; set; } = new List<InvoiceModel>();
    }
}
