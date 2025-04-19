using StoreManager.Application.Invoice.Import.DTO.Search;
using StoreManager.Application.Invoice.Import.DTO.Statistics;
using StoreManager.Application.Shared;
using StoreManager.Domain.Document.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Import.Service
{
    public interface IImportService
    {
        Task Create(Guid id, List<ExtractionMetadata> metadata);
        Task<PaginatedResult<ImportInvoiceSearchResponseDto>> FindFilteredInvoices(string? componentInfo, string? providerId, string? date1, int pageNumber, int pageSize);
        Task<ThisWeekInvoiceCountResponseDto> CountInvoicesThisWeek();
        Task<FindCountsForWeekResponseDto> FindCountsForWeek();
    }
}
