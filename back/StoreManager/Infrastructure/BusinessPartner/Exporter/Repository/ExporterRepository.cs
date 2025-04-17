using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Model;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.BusinessPartner.Exporter.Repository;

public class ExporterRepository(WarehouseDbContext context) : IExporterRepository
{
    private readonly WarehouseDbContext _context = context;
    private readonly DbSet<ExporterModel> _exporters = context.Exporters;

    public Task<ExporterModel?> FindById(Guid id)
    {
        return _exporters.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public async Task<ExporterModel> Create(ExporterModel exporter)
    {
        var savedInstance = await _exporters.AddAsync(exporter);
        await _context.SaveChangesAsync();
        return savedInstance.Entity;
    }

    public Task<List<ExporterModel>> FindAll()
    {
        return _exporters.ToListAsync();
    }

    public async Task<(ICollection<ExporterModel> Items, int TotalCount)> FindFiltered(string? exporterInfo, int pageNumber,
        int pageSize)
    {
        var query = _exporters.Include(e=>e.Exports).AsQueryable();
        if (!string.IsNullOrEmpty(exporterInfo) && !string.IsNullOrWhiteSpace(exporterInfo))
        {
            query = query.Where(e =>
                e.Name.ToLower().Contains(exporterInfo.ToLower()) ||
                e.PhoneNumber.ToLower().Contains(exporterInfo.ToLower()) ||
                e.Address.ToLower().Contains(exporterInfo.ToLower()));
        }

        var count = await query.CountAsync();
        var skip = (pageNumber - 1) * pageSize;
        query = query.Skip(skip).Take(pageSize);
        var result = await query.ToListAsync();
        
        return (result, count);
    }
}