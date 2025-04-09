using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Quantity;

namespace StoreManager.Infrastructure.MechanicalComponent.Command
{
    public record FindComponentQuantitySumQuery : IRequest<MechanicalComponentQuantitySumResponseDTO>;

}
