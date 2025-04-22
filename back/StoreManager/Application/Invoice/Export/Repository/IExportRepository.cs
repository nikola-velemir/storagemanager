using StoreManager.Domain.Invoice.Export.Specification;
using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Application.Invoice.Export.Repository;

public interface IExportRepository
{
    Task<ExportModel?> FindByIdAsync(Guid id);
    Task<ExportModel> CreateAsync(ExportModel exportModel);

    Task<(ICollection<ExportModel> Items, int TotalCount)> FindFilteredAsync(FindFilteredExportsSpecification spec,
        Guid? exporterId,
        string? productInfo, DateOnly? date, int pageNumber, int pageSize);

    Task<List<ExportModel>> FindByExporterIdAsync(Guid partnerId);
}