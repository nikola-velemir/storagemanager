using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Find;

namespace StoreManager.Infrastructure.MechanicalComponent.Command
{
    public record FindComponentByInvoiceIdQuery(string invoiceId): IRequest<MechanicalComponentFindResponsesDTO>;
}
