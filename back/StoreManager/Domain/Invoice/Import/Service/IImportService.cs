using StoreManager.Application.Invoice.Import.DTO.Search;
using StoreManager.Application.Invoice.Import.DTO.Statistics;
using StoreManager.Application.Shared;
using StoreManager.Domain.Document.Model;

namespace StoreManager.Domain.Invoice.Import.Service;

public interface IImportService
{
    Task Create(Infrastructure.Invoice.Import.Model.Import invoice, List<ExtractionMetadata> metadata);

    Task<PaginatedResult<ImportInvoiceSearchResponseDto>> FindFilteredInvoices(string? componentInfo,
        string? providerId, string? date1, int pageNumber, int pageSize);

    Task<ThisWeekInvoiceCountResponseDto> CountInvoicesThisWeek();
    Task<FindCountsForWeekResponseDto> FindCountsForWeek();
}