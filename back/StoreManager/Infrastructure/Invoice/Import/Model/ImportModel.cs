using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Provider.Model;

namespace StoreManager.Infrastructure.Invoice.Import.Model
{
    public class ImportModel : InvoiceModel
    {
        public required ProviderModel Provider { get; set; }

        public required Guid ProviderId { get; set; }
        public ICollection<ImportItemModel> Items { get; set; } = new List<ImportItemModel>();
    }
}