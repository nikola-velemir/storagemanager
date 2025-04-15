using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.Invoice.Export.Repository;

public interface IExportRepository
{
    Task<ExportModel?> FindById(Guid id);
}