using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Domain.BusinessPartner.Provider.Model
{
    public class Provider : Base.Model.BusinessPartner
    {
        
        public ICollection<Import> Imports { get; set; } = new List<Import>();

        public void AddImport(Import import)
        {
            Imports.Add(import);
        }
    }
}