using MediatR;
using StoreManager.Application.MechanicalComponent.Command.Statistics;
using StoreManager.Application.MechanicalComponent.DTO.Quantity;
using StoreManager.Application.MechanicalComponent.Repository;

namespace StoreManager.Application.MechanicalComponent.Handler.Statistics
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
