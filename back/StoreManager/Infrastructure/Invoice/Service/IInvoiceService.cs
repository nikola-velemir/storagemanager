using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Invoice.DTO.Search;
using StoreManager.Infrastructure.Invoice.DTO.Statistics;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Service
{
    public interface IInvoiceService
    {
        Task Create(Guid id, List<ExtractionMetadata> metadata);
        Task<PaginatedResult<InvoiceSearchResponseDto>> FindFilteredInvoices(string? componentInfo, string? providerId, string? date1, int pageNumber, int pageSize);
        Task<ThisWeekInvoiceCountResponseDto> CountInvoicesThisWeek();
        Task<FindCountsForWeekResponseDto> FindCountsForWeek();
    }
}
