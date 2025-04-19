using StoreManager.Domain.BusinessPartner.Exporter.Model;
using StoreManager.Infrastructure.Invoice.Base;

namespace StoreManager.Infrastructure.Invoice.Export.Model;

public class ExportModel : InvoiceModel
{
    public required ExporterModel Exporter { get; set; }
    public required Guid ExporterId { get; set; }
    public ICollection<ExportItemModel> Items { get; set; } = new List<ExportItemModel>();
}