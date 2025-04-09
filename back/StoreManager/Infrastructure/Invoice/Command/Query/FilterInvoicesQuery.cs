using MediatR;
using StoreManager.Infrastructure.Invoice.DTO;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Command.Query
{
    public record FilterInvoicesQuery(
        string? ComponentInfo,
        string? ProviderId, 
        string? DateIssued,
        int PageNumber,
        int PageSize) : IRequest<PaginatedResult<InvoiceSearchResponseDTO>>;
}
