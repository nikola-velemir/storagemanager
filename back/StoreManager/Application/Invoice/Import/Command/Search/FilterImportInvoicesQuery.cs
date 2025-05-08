using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Invoice.Import.DTO.Search;
using StoreManager.Application.Shared;

namespace StoreManager.Application.Invoice.Import.Command.Search
{
    public record FilterImportInvoicesQuery(
        string? ComponentInfo,
        string? ProviderId, 
        string? DateIssued,
        int PageNumber,
        int PageSize) : IRequest<Result<PaginatedResult<ImportInvoiceSearchResponseDto>>>;
}
