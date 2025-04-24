using Microsoft.EntityFrameworkCore;
using StoreManager.Application.BusinessPartner.Exporter.Repository;
using StoreManager.Domain.BusinessPartner.Exporter.Model;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.DB;

namespace StoreManager.Infrastructure.BusinessPartner.Exporter.Repository;

public class ExporterRepository(WarehouseDbContext context) : IExporterRepository
{
    private readonly WarehouseDbContext _context = context;
    private readonly DbSet<Domain.BusinessPartner.Exporter.Model.Exporter> _exporters = context.Exporters;

    public Task<Domain.BusinessPartner.Exporter.Model.Exporter?> FindById(Guid id)
    {
        return _exporters.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public async Task<Domain.BusinessPartner.Exporter.Model.Exporter> Create(Domain.BusinessPartner.Exporter.Model.Exporter exporter)
    {
        var savedInstance = await _exporters.AddAsync(exporter);
        await _context.SaveChangesAsync();
        return savedInstance.Entity;
    }

    public Task<List<Domain.BusinessPartner.Exporter.Model.Exporter>> FindAllAsync()
    {
        return _exporters.ToListAsync();
    }

    public async Task<(ICollection<Domain.BusinessPartner.Exporter.Model.Exporter> Items, int TotalCount)> FindFiltered(string? exporterInfo,
        int pageNumber,
        int pageSize)
    {
        var query = _exporters.Include(e => e.Exports).AsQueryable();
        if (!string.IsNullOrEmpty(exporterInfo) && !string.IsNullOrWhiteSpace(exporterInfo))
        {
            query = query.Where(e =>
                e.Name.ToLower().Contains(exporterInfo.ToLower()) ||
                e.PhoneNumber.ToLower().Contains(exporterInfo.ToLower()) ||
                e.Address.Street.ToLower().Contains(exporterInfo.ToLower()) ||
                e.Address.City.ToLower().Contains(exporterInfo.ToLower()) ||
                e.Address.StreetNumber.ToLower().Contains(exporterInfo.ToLower()));
        }

        var count = await query.CountAsync();
        var skip = (pageNumber - 1) * pageSize;
        query = query.Skip(skip).Take(pageSize);
        var result = await query.ToListAsync();

        return (result, count);
    }

    public async Task<int> FindInvoiceCountForProviderAsync(Domain.BusinessPartner.Exporter.Model.Exporter exporter)
    {
        var query = await _exporters.Include(e => e.Exports)
            .FirstOrDefaultAsync(e => e.Id.Equals(exporter.Id));

        return query?.Exports.Count ?? 0;
    }

    public async Task<int> FindProductCountForExporterAsync(Domain.BusinessPartner.Exporter.Model.Exporter exporter)
    {
        var query = await _exporters.Where(e => e.Id.Equals(exporter.Id))
            .Include(e => e.Exports)
            .ThenInclude(e => e.Items)
            .ThenInclude(ei => ei.Product).FirstOrDefaultAsync();
        
        if (query is null) return 0;

        return query.Exports.SelectMany(e => e.Items).Sum(ei => ei.Quantity);
    }
}