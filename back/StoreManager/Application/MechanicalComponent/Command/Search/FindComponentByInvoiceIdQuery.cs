using MediatR;
using StoreManager.Application.MechanicalComponent.DTO.Find;

namespace StoreManager.Application.MechanicalComponent.Command.Search
{
    public record FindComponentByInvoiceIdQuery(string InvoiceId) : IRequest<MechanicalComponentFindResponsesDto>;
}
