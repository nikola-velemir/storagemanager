using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Infrastructure.Provider.Model
{
    public class ProviderModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Adress { get; set; }
        public required string PhoneNumber { get; set; }
        public ICollection<ImportModel> Invoices { get; set; } = new List<ImportModel>();
    }
}
