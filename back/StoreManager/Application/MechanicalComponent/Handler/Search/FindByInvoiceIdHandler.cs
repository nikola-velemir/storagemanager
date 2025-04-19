using MediatR;
using StoreManager.Application.MechanicalComponent.Command.Search;
using StoreManager.Application.MechanicalComponent.DTO.Find;
using StoreManager.Application.MechanicalComponent.Repository;

namespace StoreManager.Application.MechanicalComponent.Handler.Search
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
            var result = await repository.FindByInvoiceIdAsync(invoiceGuid);

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
