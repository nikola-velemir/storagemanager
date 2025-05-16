using MediatR;
using StoreManager.Application.BusinessPartner.Provider.DTO.Search;
using StoreManager.Application.Common;
using StoreManager.Application.Shared;

namespace StoreManager.Application.BusinessPartner.Provider.Command.Search
{
    public record FindFilteredProvidersQuery(
        string? ProviderName, 
        int PageNumber, 
        int PageSize) : IRequest<Result<PaginatedResult<ProviderSearchResponseDto>>>;
}
