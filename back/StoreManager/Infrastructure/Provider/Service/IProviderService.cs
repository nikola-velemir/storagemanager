using StoreManager.Infrastructure.Provider.DTO;

namespace StoreManager.Infrastructure.Provider.Service
{
    public interface IProviderService
    {
        Task<ProviderFindResponseDTO?> FindById(Guid id);

        Task<ProviderFindResponsesDTO> FindAll();
        Task<ProviderFindResponseDTO> Create(ProviderCreateRequestDTO request);
    }
}
