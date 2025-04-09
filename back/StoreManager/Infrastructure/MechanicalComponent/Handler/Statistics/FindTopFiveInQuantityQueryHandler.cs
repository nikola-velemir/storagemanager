using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.Command.Statistics;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Quantity;
using StoreManager.Infrastructure.MechanicalComponent.Repository;

namespace StoreManager.Infrastructure.MechanicalComponent.Handler.Statistics
{
    public class FindTopFiveInQuantityQueryHandler : IRequestHandler<FindTopFiveInQuantityQuery, MechanicalComponentTopFiveQuantityResponsesDTO>
    {
        private IMechanicalComponentRepository _repository;
        public FindTopFiveInQuantityQueryHandler(IMechanicalComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<MechanicalComponentTopFiveQuantityResponsesDTO> Handle(FindTopFiveInQuantityQuery request, CancellationToken cancellationToken)
        {

            var result = await _repository.FindTopFiveInQuantity();
            var responses = new List<MechanicalComponentTopFiveQuantityResponseDTO>();
            foreach (var r in result)
            {
                var quantity = await _repository.CountQuantity(r);
                responses.Add(new MechanicalComponentTopFiveQuantityResponseDTO(r.Id,r.Name, r.Identifier, quantity));
            }
            return new MechanicalComponentTopFiveQuantityResponsesDTO(responses);
        }
    }
}
