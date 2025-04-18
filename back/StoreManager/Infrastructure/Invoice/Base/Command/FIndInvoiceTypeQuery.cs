using MediatR;
using StoreManager.Infrastructure.Invoice.Base.DTO;

namespace StoreManager.Infrastructure.Invoice.Base.Command;

public record FIndInvoiceTypeQuery(string Id) : IRequest<InvoiceFindTypeResponseDto>;
