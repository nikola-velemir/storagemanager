using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.Command.Statistics;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Quantity;
using StoreManager.Infrastructure.MechanicalComponent.Repository;

namespace StoreManager.Infrastructure.MechanicalComponent.Handler.Statistics
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
