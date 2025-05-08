using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.MechanicalComponent.DTO.Find;

namespace StoreManager.Application.MechanicalComponent.Command.Search
{
    public record FindComponentByInvoiceIdQuery(string InvoiceId) : IRequest<Result<MechanicalComponentFindResponsesDto>>;
}
