using StoreManager.Domain.BusinessPartner.Exporter.Model;
using StoreManager.Domain.Invoice.Base.Model;
using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Domain.Invoice.Export.Model;

public class Export : Base.Model.Invoice
{
    public required Exporter Exporter { get; set; }
    public required Guid ExporterId { get; set; }
    public ICollection<ExportItemModel> Items { get; set; } = new List<ExportItemModel>();

    public void AddItem(ExportItemModel item)
    {
        Items.Add(item);
    }
}