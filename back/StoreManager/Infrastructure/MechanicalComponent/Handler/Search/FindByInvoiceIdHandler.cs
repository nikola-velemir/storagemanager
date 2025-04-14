using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.Command.Search;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Find;
using StoreManager.Infrastructure.MechanicalComponent.Repository;

namespace StoreManager.Infrastructure.MechanicalComponent.Handler.Search
{
    public class FindByInvoiceIdHandler(IMechanicalComponentRepository repository)
        : IRequestHandler<FindComponentByInvoiceIdQuery, MechanicalComponentFindResponsesDto>
    {
        public async Task<MechanicalComponentFindResponsesDto> Handle(FindComponentByInvoiceIdQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.InvoiceId, out _))
            {
                throw new InvalidCastException("Guid cannot be parsed");
            }
            Guid invoiceGuid = Guid.Parse(request.InvoiceId);
            var result = await repository.FindByInvoiceId(invoiceGuid);

            return new MechanicalComponentFindResponsesDto(
                result.Select(mc =>
                new MechanicalComponentFindResponseDto(
                    mc.Id,
                    mc.Identifier,
                    mc.Name,
                    mc.Items.First(ii => ii.ImportId.Equals(invoiceGuid)).Quantity,
                    mc.Items.First(ii => ii.ImportId.Equals(invoiceGuid)).PricePerPiece
                    )
                ).ToList());
        }
    }
}
