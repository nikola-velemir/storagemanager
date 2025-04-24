using MediatR;
using StoreManager.Application.MechanicalComponent.Command;
using StoreManager.Application.MechanicalComponent.DTO.Search;
using StoreManager.Application.MechanicalComponent.Repository;

namespace StoreManager.Application.MechanicalComponent.Handler;

public class FinComponentsByPartnerQueryHandler(IMechanicalComponentRepository repository)
    : IRequestHandler<FindComponentsByPartnerQuery, List<MechanicalComponentProductSearchResponseDto>>
{
    public async Task<List<MechanicalComponentProductSearchResponseDto>> Handle(FindComponentsByPartnerQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var providerId))
            throw new InvalidCastException("Could not cast");

        var components = await repository.FindByProviderIdAsync(providerId);

        return components.Select(mc => new MechanicalComponentProductSearchResponseDto(mc.Id, mc.Identifier, mc.Name))
            .ToList();
    }
}