using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Search;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.MechanicalComponent.Command.Search
{
    public record FindFilteredComponentsQuery(string? ProviderId, string? ComponentInfo, int PageNumber, int PageSize) : IRequest<PaginatedResult<MechanicalComponentSearchResponseDTO>>;
}
