using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Utils;

namespace StoreManager.Infrastructure.Invoice.Import.Repository
{
    public sealed class ImportRepository(WarehouseDbContext context) : IImportRepository
    {
        private readonly DbSet<ImportModel> _imports = context.Imports;

        public async Task<ImportModel> Create(ImportModel import)
        {
            var savedInstance = await _imports.AddAsync(import);
            await context.SaveChangesAsync();
            return savedInstance.Entity;
        }

        public async Task<List<ImportModel>> FindAll()
        {
            return await _imports
                .Include(invoice => invoice.Provider)
                .Include(invoice => invoice.Items)
                .ThenInclude(item => item.Component).ToListAsync();
        }

        public async Task<(ICollection<ImportModel> Items, int TotalCount)> FindFiltered(string? componentInfo, Guid? providerId, DateOnly? dateIssued, int pageNumber, int pageSize)
        {
            var query = _imports.Include(i => i.Provider).Include(i => i.Items).ThenInclude(item => item.Component).AsQueryable();
            if (!string.IsNullOrEmpty(componentInfo))
            {
                query = query.Where(i => i.Items.Any(ii => ii.Component.Name.ToLower().Contains(componentInfo) || ii.Component.Identifier.Contains(componentInfo)));
            }
            if (providerId.HasValue)
            {
                query = query.Where(i => i.Provider.Id.Equals(providerId));
            }
            if (dateIssued.HasValue)
            {
                query = query.Where(i => i.DateIssued.Equals(dateIssued));
            }
            int skip = (pageNumber - 1) * pageSize;

            var totalCount = await query.CountAsync();
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return (items, totalCount);
        }

        public Task<ImportModel?> FindByDocumentId(Guid documentId)
        {
            return _imports.FirstOrDefaultAsync(i => i.DocumentId.Equals(documentId));
        }

        public Task<ImportModel?> FindById(Guid id)
        {
            return _imports.Include(i => i.Document).FirstOrDefaultAsync(i => i.Id.Equals(id));
        }

        public async Task<List<ImportModel>> FindByProviderId(Guid id)
        {
            var query = _imports.Include(i => i.Provider).Where(i => i.Provider.Id.Equals(id)).AsQueryable();
            return await query.ToListAsync();
        }
        public Task<int> CountImportsThisWeek()
        {
            var startOfWeek = DateOnly.FromDateTime(DateTime.UtcNow.StartOfWeek());
            var endOfWeek = startOfWeek.AddDays(7);

            var query = _imports.Where(i => i.DateIssued >= startOfWeek && i.DateIssued < endOfWeek);
            return query.CountAsync();
        }

        public Task<int> FindCountForTheDate(DateOnly date)
        {
            var query = _imports.Where(i => i.DateIssued.Equals(date));
            return query.CountAsync();
        }
    }
}
