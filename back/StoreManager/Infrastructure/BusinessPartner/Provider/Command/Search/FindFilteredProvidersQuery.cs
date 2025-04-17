using MediatR;
using StoreManager.Infrastructure.BusinessPartner.Provider.DTO.Search;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.BusinessPartner.Provider.Command.Search
{
    public record FindFilteredProvidersQuery(
        string? ProviderName, 
        int PageNumber, 
        int PageSize) : IRequest<PaginatedResult<ProviderSearchResponseDto>>;
}
