using StoreManager.Infrastructure.BusinessPartner.Provider.DTO;
using StoreManager.Infrastructure.BusinessPartner.Provider.DTO.Search;
using StoreManager.Infrastructure.BusinessPartner.Provider.DTO.Statistics;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.BusinessPartner.Provider.Service
{
    public interface IProviderService
    {
        Task<ProviderFindResponseDto?> FindById(Guid id);

        Task<ProviderFindResponsesDto> FindAll();
        Task<ProviderFindResponseDto> Create(ProviderCreateRequestDto request);
        Task<PaginatedResult<ProviderSearchResponseDto>> FindFiltered(string? providerName, int pageNumber, int pageSize);
        Task<ProviderProfileResponseDto> FindProfile(string providerId);
        Task<ProviderInvoiceInvolvementResponsesDto> FindProviderInvoiceInvolements();
        Task<ProviderComponentInvolvementResponsesDto> FindProviderComponentInvolements();

    }
}
