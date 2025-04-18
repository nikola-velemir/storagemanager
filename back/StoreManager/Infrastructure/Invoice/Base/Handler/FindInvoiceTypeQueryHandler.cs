using MediatR;
using StoreManager.Infrastructure.Invoice.Base.Command;
using StoreManager.Infrastructure.Invoice.Base.DTO;
using StoreManager.Infrastructure.Invoice.Base.Repository;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Infrastructure.Invoice.Base.Handler;

public class FindInvoiceTypeQueryHandler(IInvoiceRepository repository)
    : IRequestHandler<FIndInvoiceTypeQuery, InvoiceFindTypeResponseDto>
{
    public async Task<InvoiceFindTypeResponseDto> Handle(FIndInvoiceTypeQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out _))
            throw new InvalidCastException("Could not cast Guid");

        var invoiceId = Guid.Parse(request.Id);
        var invoice = await repository.FindById(invoiceId) ?? throw new NotFoundException("Could not find Invoice");

        return new InvoiceFindTypeResponseDto(invoice.Type.ToString());
    }
}