using MediatR;
using StoreManager.Infrastructure.Product.DTO;

namespace StoreManager.Infrastructure.Product.Command;

public record FindProductByInvoiceIdQuery(string InvoiceId) : IRequest<FindProductByInvoiceIdResponsesDto>;
