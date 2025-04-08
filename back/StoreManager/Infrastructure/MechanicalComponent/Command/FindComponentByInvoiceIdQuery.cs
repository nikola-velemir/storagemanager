using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Find;

namespace StoreManager.Infrastructure.MechanicalComponent.Command
{
    public record FindComponentByInvoiceIdQuery(string InvoiceId) : IRequest<MechanicalComponentFindResponsesDTO>;
}
