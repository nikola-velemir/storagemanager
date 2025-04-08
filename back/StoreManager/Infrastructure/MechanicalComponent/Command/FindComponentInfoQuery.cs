using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Info;

namespace StoreManager.Infrastructure.MechanicalComponent.Command
{
    public record FindComponentInfoQuery(string ComponentId) : IRequest<MechanicalComponentInfoResponseDTO>;
}
