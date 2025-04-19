using MediatR;
using StoreManager.Application.MechanicalComponent.Command.Search;
using StoreManager.Application.MechanicalComponent.DTO.Search;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Application.Shared;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.MechanicalComponent.Handler.Search;

public class FindFilteredComponentsForProductQueryHandler(IMechanicalComponentRepository repository) : IRequestHandler<FindFilteredComponentsForProductQuery, PaginatedResult<MechanicalComponentProductSearchResponseDto>>
{
    
    public async Task<PaginatedResult<MechanicalComponentProductSearchResponseDto>> Handle(FindFilteredComponentsForProductQuery request, CancellationToken cancellationToken)
    {
        Guid? id = null;
        if (Guid.TryParse(request.ProviderId, out var tempId))
        {
            id = tempId;
        }

        var result = await repository.FindFilteredForProductAsync(id, request.ComponentInfo, request.PageNumber, request.PageSize);
        return new PaginatedResult<MechanicalComponentProductSearchResponseDto>
        {
            Items = result.Items.Select(mc =>
                    new MechanicalComponentProductSearchResponseDto(
                        mc.Id,
                        mc.Identifier,
                        mc.Name
                    )
                )
                .ToList(),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = result.TotalCount
        };
    }
}