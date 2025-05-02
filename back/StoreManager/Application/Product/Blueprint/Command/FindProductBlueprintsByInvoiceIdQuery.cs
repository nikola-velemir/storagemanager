using MediatR;
using StoreManager.Application.Product.Blueprint.DTO;

namespace StoreManager.Application.Product.Blueprint.Command;

public record FindProductBlueprintsByInvoiceIdQuery(string InvoiceId) : IRequest<FindProductByInvoiceIdResponsesDto>;
