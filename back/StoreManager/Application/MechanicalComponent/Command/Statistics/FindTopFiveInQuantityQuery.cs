using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.MechanicalComponent.DTO.Quantity;

namespace StoreManager.Application.MechanicalComponent.Command.Statistics
{
    public record FindTopFiveInQuantityQuery() : IRequest<Result<MechanicalComponentTopFiveQuantityResponsesDto>>;
}