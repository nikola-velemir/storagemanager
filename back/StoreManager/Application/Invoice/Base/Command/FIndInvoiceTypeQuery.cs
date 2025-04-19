using MediatR;
using StoreManager.Application.Invoice.Base.DTO;

namespace StoreManager.Application.Invoice.Base.Command;

public record FIndInvoiceTypeQuery(string Id) : IRequest<InvoiceFindTypeResponseDto>;
