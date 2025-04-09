using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.Command;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Quantity;
using StoreManager.Infrastructure.MechanicalComponent.Repository;

namespace StoreManager.Infrastructure.MechanicalComponent.Handler
{
    public class FindComponentQuantitySumHandler : IRequestHandler<FindComponentQuantitySumQuery, MechanicalComponentQuantitySumResponseDTO>
    {
        private IMechanicalComponentRepository _repository;
        public FindComponentQuantitySumHandler(IMechanicalComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<MechanicalComponentQuantitySumResponseDTO> Handle(FindComponentQuantitySumQuery request, CancellationToken cancellationToken)
        {
            return new MechanicalComponentQuantitySumResponseDTO(await _repository.FindQuantitySum());
        }
    }
}
