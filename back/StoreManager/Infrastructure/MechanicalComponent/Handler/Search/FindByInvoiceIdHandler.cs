using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.Command.Search;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Find;
using StoreManager.Infrastructure.MechanicalComponent.Repository;

namespace StoreManager.Infrastructure.MechanicalComponent.Handler.Search
{
    public class FindByInvoiceIdHandler : IRequestHandler<FindComponentByInvoiceIdQuery, MechanicalComponentFindResponsesDTO>
    {
        private IMechanicalComponentRepository _repository;

        public FindByInvoiceIdHandler(IMechanicalComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<MechanicalComponentFindResponsesDTO> Handle(FindComponentByInvoiceIdQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.InvoiceId, out _))
            {
                throw new InvalidCastException("Guid cannot be parsed");
            }
            Guid invoiceGuid = Guid.Parse(request.InvoiceId);
            var result = await _repository.FindByInvoiceId(invoiceGuid);

            return new MechanicalComponentFindResponsesDTO(
                result.Select(mc =>
                new MechanicalComponentFindResponseDTO(
                    mc.Id,
                    mc.Identifier,
                    mc.Name,
                    mc.Items.First(ii => ii.InvoiceId.Equals(invoiceGuid)).Quantity,
                    mc.Items.First(ii => ii.InvoiceId.Equals(invoiceGuid)).PricePerPiece
                    )
                ).ToList());
        }
    }
}
