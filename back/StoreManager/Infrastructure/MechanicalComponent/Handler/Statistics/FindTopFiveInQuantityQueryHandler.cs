using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.Command.Statistics;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Quantity;
using StoreManager.Infrastructure.MechanicalComponent.Repository;

namespace StoreManager.Infrastructure.MechanicalComponent.Handler.Statistics
{
    public class FindTopFiveInQuantityQueryHandler(IMechanicalComponentRepository repository)
        : IRequestHandler<FindTopFiveInQuantityQuery, MechanicalComponentTopFiveQuantityResponsesDto>
    {
        public async Task<MechanicalComponentTopFiveQuantityResponsesDto> Handle(FindTopFiveInQuantityQuery request, CancellationToken cancellationToken)
        {

            var result = await repository.FindTopFiveInQuantity();
            var responses = new List<MechanicalComponentTopFiveQuantityResponseDto>();
            foreach (var r in result)
            {
                var quantity = await repository.CountQuantity(r);
                responses.Add(new MechanicalComponentTopFiveQuantityResponseDto(r.Id,r.Name, r.Identifier, quantity));
            }
            return new MechanicalComponentTopFiveQuantityResponsesDto(responses);
        }
    }
}
