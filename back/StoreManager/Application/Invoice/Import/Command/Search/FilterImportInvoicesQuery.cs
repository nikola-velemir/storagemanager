using MediatR;
using StoreManager.Application.Invoice.Import.DTO.Search;
using StoreManager.Application.Shared;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Invoice.Import.Command.Search
{
    public record FilterImportInvoicesQuery(
        string? ComponentInfo,
        string? ProviderId, 
        string? DateIssued,
        int PageNumber,
        int PageSize) : IRequest<PaginatedResult<ImportInvoiceSearchResponseDto>>;
}
