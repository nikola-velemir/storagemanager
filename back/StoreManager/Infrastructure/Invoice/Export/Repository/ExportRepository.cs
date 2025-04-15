using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.Invoice.Export.Repository;

public class ExportRepository(WarehouseDbContext context) : IExportRepository

{
    private readonly DbSet<ExportModel> _exports = context.Exports;

    public Task<ExportModel?> FindById(Guid id)
    {
        return _exports.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }
}