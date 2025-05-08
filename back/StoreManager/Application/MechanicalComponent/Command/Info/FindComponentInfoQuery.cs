using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.MechanicalComponent.DTO.Info;

namespace StoreManager.Application.MechanicalComponent.Command.Info
{
    public record FindComponentInfoQuery(string ComponentId) : IRequest<Result<MechanicalComponentInfoResponseDto>>;
}