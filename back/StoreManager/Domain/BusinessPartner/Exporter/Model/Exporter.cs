using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.Invoice.Export.Model;

namespace StoreManager.Domain.BusinessPartner.Exporter.Model;

public class Exporter : Base.Model.BusinessPartner
{
    public ICollection<Export> Exports { get; set; } = new List<Export>();
}