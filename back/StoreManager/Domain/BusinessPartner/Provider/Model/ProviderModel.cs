using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Domain.BusinessPartner.Provider.Model
{
    public class ProviderModel : BusinessPartnerModel
    {
        
        public ICollection<ImportModel> Imports { get; set; } = new List<ImportModel>();
    }
}