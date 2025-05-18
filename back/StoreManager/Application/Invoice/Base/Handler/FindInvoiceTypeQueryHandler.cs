using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Invoice.Base.Command;
using StoreManager.Application.Invoice.Base.DTO;
using StoreManager.Application.Invoice.Base.Error;
using StoreManager.Domain.Invoice.Base.Repository;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.Invoice.Base.Handler;

public class FindInvoiceTypeQueryHandler(IInvoiceRepository repository)
    : IRequestHandler<FindInvoiceTypeQuery, Result<InvoiceFindTypeResponseDto>>
{
    public async Task<Result<InvoiceFindTypeResponseDto>> Handle(FindInvoiceTypeQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out _))
            return InvoiceErrors.InvoiceIdParseError;

        var invoiceId = Guid.Parse(request.Id);
        var invoice = await repository.FindById(invoiceId);
        if (invoice is null)
            return InvoiceErrors.InvoiceNotFound;

        return Result.Success(new InvoiceFindTypeResponseDto(invoice.Type.ToString(), invoice.Document.IsProcessed));
    }
}