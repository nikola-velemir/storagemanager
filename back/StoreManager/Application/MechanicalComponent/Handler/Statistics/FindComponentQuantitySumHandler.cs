using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.MechanicalComponent.Command.Statistics;
using StoreManager.Application.MechanicalComponent.DTO.Quantity;
using StoreManager.Application.MechanicalComponent.Repository;

namespace StoreManager.Application.MechanicalComponent.Handler.Statistics
{
    public class FindComponentQuantitySumHandler(IMechanicalComponentRepository repository)
        : IRequestHandler<FindComponentQuantitySumQuery, Result<MechanicalComponentQuantitySumResponseDto>>
    {
        public async Task<Result<MechanicalComponentQuantitySumResponseDto>> Handle(
            FindComponentQuantitySumQuery request, CancellationToken cancellationToken)
        {
            return Result.Success(
                new MechanicalComponentQuantitySumResponseDto(await repository.FindQuantitySumAsync()));
        }
    }
}