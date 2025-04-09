using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Find;

namespace StoreManager.Infrastructure.MechanicalComponent.Command.Search
{
    public record FindComponentByInvoiceIdQuery(string InvoiceId) : IRequest<MechanicalComponentFindResponsesDTO>;
}
