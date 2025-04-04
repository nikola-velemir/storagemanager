using StoreManager.Infrastructure.Provider.Model;

namespace StoreManager.Infrastructure.Provider.Repository
{
    public interface IProviderRepository
    {
        Task<ProviderModel> Create(ProviderModel provider);
        Task<List<ProviderModel>> FindAll();
        Task<ProviderModel?> FindById(Guid id);
    }
}
