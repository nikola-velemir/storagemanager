using StoreManager.Infrastructure.Provider.DTO;
using StoreManager.Infrastructure.Provider.DTO.Search;
using StoreManager.Infrastructure.Provider.DTO.Statistics;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Provider.Service
{
    public interface IProviderService
    {
        Task<ProviderFindResponseDTO?> FindById(Guid id);

        Task<ProviderFindResponsesDTO> FindAll();
        Task<ProviderFindResponseDTO> Create(ProviderCreateRequestDTO request);
        Task<PaginatedResult<ProviderSearchResponseDTO>> FindFiltered(string? providerName, int pageNumber, int pageSize);
        Task<ProviderProfileResponseDTO> FindProfile(string providerId);
        Task<ProviderInvoiceInvolvementResponsesDTO> FindProviderInvoiceInvolements();
        Task<ProviderComponentInvolvementResponsesDTO> FindProviderComponentInvolements();

    }
}
