using MediatR;
using StoreManager.Infrastructure.Invoice.Import.DTO.Search;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Import.Command.Search
{
    public record FilterImportInvoicesQuery(
        string? ComponentInfo,
        string? ProviderId, 
        string? DateIssued,
        int PageNumber,
        int PageSize) : IRequest<PaginatedResult<ImportInvoiceSearchResponseDto>>;
}
