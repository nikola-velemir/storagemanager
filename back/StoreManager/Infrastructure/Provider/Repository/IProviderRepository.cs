using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.Provider.Model;

namespace StoreManager.Infrastructure.Provider.Repository
{
    public interface IProviderRepository
    {
        Task<ProviderModel> Create(ProviderModel provider);
        Task<ProviderModel> Update(ProviderModel provider);
        Task AddInvoice(ProviderModel provider, InvoiceModel invoice);
        Task<List<ProviderModel>> FindAll();
        Task<ProviderModel?> FindById(Guid id);
        Task<(ICollection<ProviderModel> Items, int TotalCount)> FindFiltered(string? providerName, int pageNumber, int pageSize);
        Task<int> FindInvoiceCountForProvider(ProviderModel provider);
    }
}
