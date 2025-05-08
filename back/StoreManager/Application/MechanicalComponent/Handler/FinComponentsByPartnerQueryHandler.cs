using MediatR;
using StoreManager.Application.BusinessPartner.Base.Errors;
using StoreManager.Application.Common;
using StoreManager.Application.MechanicalComponent.Command;
using StoreManager.Application.MechanicalComponent.DTO.Search;
using StoreManager.Application.MechanicalComponent.Repository;

namespace StoreManager.Application.MechanicalComponent.Handler;

public class FinComponentsByPartnerQueryHandler(IMechanicalComponentRepository repository)
    : IRequestHandler<FindComponentsByPartnerQuery, Result<List<MechanicalComponentProductSearchResponseDto>>>
{
    public async Task<Result<List<MechanicalComponentProductSearchResponseDto>>> Handle(
        FindComponentsByPartnerQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var providerId))
            return BusinessPartnerErrors.PartnerNotFoundError;

        var components = await repository.FindByProviderIdAsync(providerId);

        var response = components
            .Select(mc => new MechanicalComponentProductSearchResponseDto(mc.Id, mc.Identifier, mc.Name))
            .ToList();

        return Result.Success(response);
    }
}