using DocumentFormat.OpenXml.Wordprocessing;
using MediatR;
using StoreManager.Infrastructure.Provider.DTO.Search;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Provider.Command.Search
{
    public record FindFilteredProvidersQuery(
        string? ProviderName, 
        int PageNumber, 
        int PageSize) : IRequest<PaginatedResult<ProviderSearchResponseDto>>;
}
