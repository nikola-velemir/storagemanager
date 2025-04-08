using DocumentFormat.OpenXml.Wordprocessing;
using MediatR;
using StoreManager.Infrastructure.Provider.DTO;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Provider.Command
{
    public record FindFilteredProvidersQuery(
        string? ProviderName, 
        int PageNumber, 
        int PageSize) : IRequest<PaginatedResult<ProviderSearchResponseDTO>>;
}
