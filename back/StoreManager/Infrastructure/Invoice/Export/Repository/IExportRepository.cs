using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.Invoice.Export.Repository;

public interface IExportRepository
{
    Task<ExportModel?> FindById(Guid id);
    Task<ExportModel> Create(ExportModel exportModel);

    Task<(ICollection<ExportModel> Items, int TotalCount)> FindFiltered(int pageNumber, int pageSize);
}