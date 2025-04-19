using MediatR;
using StoreManager.Application.MechanicalComponent.DTO.Info;

namespace StoreManager.Application.MechanicalComponent.Command.Info
{
    public record FindComponentInfoQuery(string ComponentId) : IRequest<MechanicalComponentInfoResponseDto>;
}
