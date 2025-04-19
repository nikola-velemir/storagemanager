using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Application.BusinessPartner.Provider.Repository
{
    public interface IProviderRepository
    {
        Task<ProviderModel> CreateAsync(ProviderModel provider);
        Task<ProviderModel> UpdateAsync(ProviderModel provider);
        Task AddInvoiceAsync(ProviderModel provider, ImportModel import);
        Task<List<ProviderModel>> FindAllAsync();
        Task<ProviderModel?> FindByIdAsync(Guid id);
        Task<(ICollection<ProviderModel> Items, int TotalCount)> FindFilteredAsync(string? providerName, int pageNumber, int pageSize);
        Task<int> FindInvoiceCountForProviderAsync(ProviderModel provider);
        Task<int> FindComponentCountForProviderAsync(ProviderModel provider);
    }
}
