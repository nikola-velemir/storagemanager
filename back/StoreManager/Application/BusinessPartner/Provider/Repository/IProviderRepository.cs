using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Application.BusinessPartner.Provider.Repository
{
    public interface IProviderRepository
    {
        Task<Domain.BusinessPartner.Provider.Model.Provider> CreateAsync(Domain.BusinessPartner.Provider.Model.Provider provider);
        Task<Domain.BusinessPartner.Provider.Model.Provider> UpdateAsync(Domain.BusinessPartner.Provider.Model.Provider provider);
        Task AddInvoiceAsync(Domain.BusinessPartner.Provider.Model.Provider provider, Import import);
        Task<List<Domain.BusinessPartner.Provider.Model.Provider>> FindAllAsync();
        Task<Domain.BusinessPartner.Provider.Model.Provider?> FindByIdAsync(Guid id);
        Task<(ICollection<Domain.BusinessPartner.Provider.Model.Provider> Items, int TotalCount)> FindFilteredAsync(string? providerName, int pageNumber, int pageSize);
        Task<int> FindInvoiceCountForProviderAsync(Domain.BusinessPartner.Provider.Model.Provider provider);
        Task<int> FindComponentCountForProviderAsync(Domain.BusinessPartner.Provider.Model.Provider provider);
    }
}
