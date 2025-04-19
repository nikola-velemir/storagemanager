using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Domain.BusinessPartner.Exporter.Model;

public class ExporterModel : BusinessPartnerModel
{
    public ICollection<ExportModel> Exports { get; set; } = new List<ExportModel>();
}