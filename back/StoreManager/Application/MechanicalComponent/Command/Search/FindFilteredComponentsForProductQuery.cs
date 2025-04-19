using MediatR;
using StoreManager.Application.MechanicalComponent.DTO.Search;
using StoreManager.Application.Shared;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.MechanicalComponent.Command.Search;

public record FindFilteredComponentsForProductQuery(
    string? ProviderId,
    string? ComponentInfo,
    int PageNumber,
    int PageSize) : IRequest<PaginatedResult<MechanicalComponentProductSearchResponseDto>>;