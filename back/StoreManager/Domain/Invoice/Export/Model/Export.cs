using StoreManager.Domain.BusinessPartner.Exporter.Model;
using StoreManager.Domain.Invoice.Base.Model;
using StoreManager.Infrastructure.Invoice.Base;

namespace StoreManager.Domain.Invoice.Export.Model;

public class Export : Base.Model.Invoice
{
    public required Exporter Exporter { get; set; }
    public required Guid ExporterId { get; set; }
    public ICollection<ExportItem> Items { get; set; } = new List<ExportItem>();

    public void AddItem(ExportItem item)
    {
        Items.Add(item);
    }
}