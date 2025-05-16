using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.MechanicalComponent.DTO.Search;
using StoreManager.Application.Shared;

namespace StoreManager.Application.MechanicalComponent.Command.Search
{
    public record FindFilteredComponentsQuery(string? ProviderId, string? ComponentInfo, int PageNumber, int PageSize)
        : IRequest<Result<PaginatedResult<MechanicalComponentSearchResponseDto>>>;
}