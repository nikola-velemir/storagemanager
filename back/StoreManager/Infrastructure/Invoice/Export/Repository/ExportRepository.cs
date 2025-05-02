using Microsoft.EntityFrameworkCore;
using StoreManager.Application.Invoice.Export.Repository;
using StoreManager.Domain.Document.Service;
using StoreManager.Domain.Invoice.Export.Model;
using StoreManager.Domain.Invoice.Export.Specification;
using StoreManager.Domain.Product.Blueprint.Model;
using StoreManager.Domain.Utils;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.Invoice.Export.Repository;

public class ExportRepository : IExportRepository

{
    private readonly DbSet<Domain.Invoice.Export.Model.Export> _exports ;
    private readonly DbSet<ProductBlueprint> _products ;

    public ExportRepository(WarehouseDbContext context)
    {
        _exports = context.Exports;
        _products = context.ProductBlueprints;
    }
    public Task<Domain.Invoice.Export.Model.Export?> FindByIdAsync(Guid id)
    {
        return _exports.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public async Task<Domain.Invoice.Export.Model.Export> CreateAsync(Domain.Invoice.Export.Model.Export export)
    {
        var savedInstance = await _exports.AddAsync(export);
        return savedInstance.Entity;
    }

    public async Task<(ICollection<Domain.Invoice.Export.Model.Export> Items, int TotalCount)> FindFilteredAsync(FindFilteredExportsSpecification spec, Guid? exporterId,
        string? productInfo, DateOnly? date, int pageNumber, int pageSize)
    {
        var query = spec.Apply(_exports);
        if (exporterId.HasValue)
            query = query.Where(e => e.ExporterId.Equals(exporterId.Value));

        if (!string.IsNullOrEmpty(productInfo))
            query =
                query.Where(e => e.Items.Any(ii =>
                    ii.Product.Description.ToLower().Contains(productInfo.ToLower()) ||
                    ii.Product.Name.ToLower().Contains(productInfo.ToLower())));

        if (date.HasValue)
            query = query.Where(e => e.DateIssued.Equals(date));

        var skip = (pageNumber - 1) * pageSize;
        var items = await query.Skip(skip).Take(pageSize).ToListAsync();
        return (items, items.Count);
    }

    public Task<List<Domain.Invoice.Export.Model.Export>> FindByExporterIdAsync(Guid partnerId)
    {
        return _exports.Where(e=>e.ExporterId.Equals(partnerId)).ToListAsync();
    }

    public async Task CreateFromProductRowsAsync(Domain.Invoice.Export.Model.Export export, List<ProductRow> productRows)
    {
        foreach (var productRow in productRows)
        {
            var product =
                await _products.FirstOrDefaultAsync(p =>
                    p.Identifier.ToLower().Equals(productRow.Identifier.ToLower()));
            if (product is null)
            {
                continue;
            }

            var item = new ExportItemModel
            {
                Product = product,
                Export = export,
                ExportId = export.Id,
                ProductId = product.Id,
                PricePerPiece = productRow.Price,
                Quantity = productRow.Quantity
            };
            export.Items.Add(item);
        }
        
    }

    public Task<int> FindCountForDateAsync(DateOnly date)
    {
        var query = _exports.Where(e => e.DateIssued.Equals(date));
        return query.CountAsync();
    }

    public Task<int> CountExportsThisWeekAsync()
    {
        
        var startOfWeek = DateOnly.FromDateTime(DateTime.UtcNow.StartOfWeek());
        var endOfWeek = startOfWeek.AddDays(7);
        
        var query = _exports.Where(e=>e.DateIssued >= startOfWeek && e.DateIssued < endOfWeek);
        return query.CountAsync();
    }
}