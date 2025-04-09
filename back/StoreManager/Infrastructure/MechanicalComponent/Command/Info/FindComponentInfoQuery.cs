using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Info;

namespace StoreManager.Infrastructure.MechanicalComponent.Command.Info
{
    public record FindComponentInfoQuery(string ComponentId) : IRequest<MechanicalComponentInfoResponseDTO>;
}
