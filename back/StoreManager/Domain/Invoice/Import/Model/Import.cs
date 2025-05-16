using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Domain.Invoice.Import.Model;

public class Import : Domain.Invoice.Base.Model.Invoice
{
    public required Provider Provider { get; set; }

    public required Guid ProviderId { get; set; }
    public ICollection<ImportItemModel> Items { get; set; } = new List<ImportItemModel>();

    public void AddItem(ImportItemModel item)
    {
        Items.Add(item);
    }
}