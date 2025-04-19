using Microsoft.EntityFrameworkCore;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Infrastructure.Invoice.Import.Repository
{
    public class ImportItemRepository : IImportItemRepository
    {
        private readonly WarehouseDbContext _context;
        private readonly DbSet<ImportItemModel> _importItems;
        public ImportItemRepository(WarehouseDbContext context)
        {
            _context = context;
            _importItems = _context.ImportItems;
        }
        public async Task<ImportItemModel> Create(ImportItemModel importItem)
        {
            var savedInstance = await _importItems.AddAsync(importItem);
            await _context.SaveChangesAsync();
            return savedInstance.Entity;
        }

        public Task<ImportItemModel?> FindByImportAndComponentId(Guid invoiceId, Guid componentId)
        {
            return _importItems.FirstOrDefaultAsync(ii => ii.ImportId.Equals(invoiceId) && ii.ComponentId.Equals(componentId));
        }

        public Task<double> FindSumForDate(DateOnly date)
        {
            var query = _importItems.Include(ii => ii.Import).Where(ii => ii.Import.DateIssued.Equals(date));
            return query.SumAsync(ii => ii.Quantity * ii.PricePerPiece);
        }

        public Task<double> FindTotalPrice()
        {
            return _importItems.Select(ii => ii.Quantity * ii.PricePerPiece).SumAsync();

        }
    }
}
