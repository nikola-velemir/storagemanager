using StoreManager.Domain.Invoice.Export.Specification;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Application.Invoice.Export.Repository;

public interface IExportRepository
{
    Task<ExportModel?> FindById(Guid id);
    Task<ExportModel> Create(ExportModel exportModel);

    Task<(ICollection<ExportModel> Items, int TotalCount)> FindFiltered(FindFilteredExportsSpecification spec,
        Guid? exporterId,
        string? productInfo, DateOnly? date, int pageNumber, int pageSize);
}