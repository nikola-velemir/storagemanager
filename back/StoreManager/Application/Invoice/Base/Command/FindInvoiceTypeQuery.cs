using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Invoice.Base.DTO;

namespace StoreManager.Application.Invoice.Base.Command;

public record FindInvoiceTypeQuery(string Id) : IRequest<Result<InvoiceFindTypeResponseDto>>;
