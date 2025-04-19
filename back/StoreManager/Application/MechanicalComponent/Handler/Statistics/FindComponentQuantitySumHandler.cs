using MediatR;
using StoreManager.Application.MechanicalComponent.Command.Statistics;
using StoreManager.Application.MechanicalComponent.DTO.Quantity;
using StoreManager.Application.MechanicalComponent.Repository;

namespace StoreManager.Application.MechanicalComponent.Handler.Statistics
{
    public class FindComponentQuantitySumHandler(IMechanicalComponentRepository repository)
        : IRequestHandler<FindComponentQuantitySumQuery, MechanicalComponentQuantitySumResponseDto>
    {
        public async Task<MechanicalComponentQuantitySumResponseDto> Handle(FindComponentQuantitySumQuery request, CancellationToken cancellationToken)
        {
            return new MechanicalComponentQuantitySumResponseDto(await repository.FindQuantitySum());
        }
    }
}
