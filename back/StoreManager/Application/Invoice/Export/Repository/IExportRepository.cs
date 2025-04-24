using StoreManager.Domain.Document.Service;
using StoreManager.Domain.Invoice.Export.Model;
using StoreManager.Domain.Invoice.Export.Specification;

namespace StoreManager.Application.Invoice.Export.Repository;

public interface IExportRepository
{
    Task<Domain.Invoice.Export.Model.Export?> FindByIdAsync(Guid id);
    Task<Domain.Invoice.Export.Model.Export> CreateAsync(Domain.Invoice.Export.Model.Export export);

    Task<(ICollection<Domain.Invoice.Export.Model.Export> Items, int TotalCount)> FindFilteredAsync(FindFilteredExportsSpecification spec,
        Guid? exporterId,
        string? productInfo, DateOnly? date, int pageNumber, int pageSize);

    Task<List<Domain.Invoice.Export.Model.Export>> FindByExporterIdAsync(Guid partnerId);
    Task CreateFromProductRowsAsync(Domain.Invoice.Export.Model.Export export, List<ProductRow> productRows);
    Task<int> FindCountForDateAsync(DateOnly date);
    Task<int> CountExportsThisWeekAsync();
}