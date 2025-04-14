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
}