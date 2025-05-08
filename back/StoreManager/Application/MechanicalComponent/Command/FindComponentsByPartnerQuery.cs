using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.MechanicalComponent.DTO.Search;

namespace StoreManager.Application.MechanicalComponent.Command;

public record FindComponentsByPartnerQuery(string Id)
    : IRequest<Result<List<MechanicalComponentProductSearchResponseDto>>>;