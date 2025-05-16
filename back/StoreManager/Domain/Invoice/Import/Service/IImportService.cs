using StoreManager.Application.Invoice.Import.DTO.Search;
using StoreManager.Application.Invoice.Import.DTO.Statistics;
using StoreManager.Application.Shared;
using StoreManager.Domain.Document.Model;

namespace StoreManager.Domain.Invoice.Import.Service;

public interface IImportService
{
    Task Create(Model.Import invoice, List<ExtractionMetadata> metadata,
        List<MechanicalComponent.Model.MechanicalComponent> components);

    Task<PaginatedResult<ImportInvoiceSearchResponseDto>> FindFilteredInvoices(string? componentInfo,
        string? providerId, string? date1, int pageNumber, int pageSize);

    Task<ThisWeekInvoiceCountResponseDto> CountInvoicesThisWeek();
    Task<FindCountsForWeekResponseDto> FindCountsForWeek();
}