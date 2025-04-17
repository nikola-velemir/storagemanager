using DocumentFormat.OpenXml.Wordprocessing;
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

    public async Task<ExportModel> Create(ExportModel exportModel)
    {
        var savedInstance = await _exports.AddAsync(exportModel);
        await context.SaveChangesAsync();
        return savedInstance.Entity;
    }

    public async Task<(ICollection<ExportModel> Items, int TotalCount)> FindFiltered(int pageNumber, int pageSize)
    {
        var query = _exports
            .Include(e=>e.Document)
            .Include(e=>e.Exporter)
            .Include(e=>e.Items)
            .ThenInclude(i=>i.Product)
            .AsQueryable();
        
        var skip = (pageNumber - 1) * pageSize;
        var items = await query.Skip(skip).Take(pageSize).ToListAsync();
        return (items, items.Count);
    }
}