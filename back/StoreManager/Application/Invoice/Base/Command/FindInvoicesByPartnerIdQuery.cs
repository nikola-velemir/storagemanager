using MediatR;
using StoreManager.Application.Invoice.Base.DTO;

namespace StoreManager.Application.Invoice.Base.Command;

public record FindInvoicesByPartnerIdQuery(string Id):IRequest<List<InvoiceFindResponseDto>>;