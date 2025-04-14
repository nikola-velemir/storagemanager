using StoreManager.Infrastructure.BusinessPartner.Base;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.BusinessPartner.Exporter.Model;

public class ExporterModel : BusinessPartnerModel
{
    public ICollection<ExportModel> Exports { get; set; } = new List<ExportModel>();
}