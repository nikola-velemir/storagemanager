using MediatR;
using StoreManager.Application.Product.DTO;

namespace StoreManager.Application.Product.Command;

public record FindProductByInvoiceIdQuery(string InvoiceId) : IRequest<FindProductByInvoiceIdResponsesDto>;
