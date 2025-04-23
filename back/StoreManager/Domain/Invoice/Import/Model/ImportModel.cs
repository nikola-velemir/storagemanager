using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Domain.Invoice.Base.Model;
using StoreManager.Infrastructure.Invoice.Base;

namespace StoreManager.Infrastructure.Invoice.Import.Model
{
    public class ImportModel : InvoiceModel
    {
        public required ProviderModel Provider { get; set; }

        public required Guid ProviderId { get; set; }
        public ICollection<ImportItemModel> Items { get; set; } = new List<ImportItemModel>();

        public void AddItem(ImportItemModel item)
        {
            Items.Add(item);
        }
    }
}