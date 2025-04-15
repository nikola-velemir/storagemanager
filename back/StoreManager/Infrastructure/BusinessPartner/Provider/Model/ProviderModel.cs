using StoreManager.Infrastructure.BusinessPartner.Base;
using StoreManager.Infrastructure.BusinessPartner.Base.Model;
using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Infrastructure.BusinessPartner.Provider.Model
{
    public class ProviderModel : BusinessPartnerModel
    {
        
        public ICollection<ImportModel> Imports { get; set; } = new List<ImportModel>();
    }
}