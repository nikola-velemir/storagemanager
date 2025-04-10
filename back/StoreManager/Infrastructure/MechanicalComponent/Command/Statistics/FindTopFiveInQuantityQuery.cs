using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Quantity;

namespace StoreManager.Infrastructure.MechanicalComponent.Command.Statistics
{
    public record FindTopFiveInQuantityQuery() : IRequest<MechanicalComponentTopFiveQuantityResponsesDto>;
}