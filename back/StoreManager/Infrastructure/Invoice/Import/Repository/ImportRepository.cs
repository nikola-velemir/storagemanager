using Microsoft.EntityFrameworkCore;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Domain.Utils;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Shared;

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

        public async Task<List<ImportModel>> FindAll(ISpecification<ImportModel> spec)
        {
            var query = spec.Apply(_imports);
            return await _imports.ToListAsync();
        }

        public async Task<(ICollection<ImportModel> Items, int TotalCount)> FindFiltered(
            ISpecification<ImportModel> spec, string? componentInfo, Guid? providerId, DateOnly? dateIssued,
            int pageNumber, int pageSize)
        {
            var query = spec.Apply(_imports);

            if (!string.IsNullOrEmpty(componentInfo))
            {
                query = query.Where(i => i.Items.Any(ii =>
                    ii.Component.Name.ToLower().Contains(componentInfo) ||
                    ii.Component.Identifier.Contains(componentInfo)));
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

        public Task<ImportModel?> FindById(ISpecification<ImportModel> spec,Guid id)
        {
            var query = spec.Apply(_imports);
            return query.FirstOrDefaultAsync(i => i.Id.Equals(id));
        }

        public async Task<List<ImportModel>> FindByProviderId(ISpecification<ImportModel> spec,Guid id)
        {
            var query = spec.Apply(_imports);
            query = query.Where(i => i.Provider.Id.Equals(id)).AsQueryable();
            return await query.ToListAsync();
        }

        public Task<int> CountImportsThisWeek()
        {
            var startOfWeek = DateOnly.FromDateTime(DateTime.UtcNow.StartOfWeek());
            var endOfWeek = startOfWeek.AddDays(7);

            var query = _imports.Where(i => i.DateIssued >= startOfWeek && i.DateIssued < endOfWeek);
            return query.CountAsync();
        }

        public Task<int> FindCountForTheDateAsync(DateOnly date)
        {
            var query = _imports.Where(i => i.DateIssued.Equals(date));
            return query.CountAsync();
        }

        public Task<ImportItemModel?> FindByImportAndComponentIdAsync(Guid invoiceId, Guid componentId)
        {
            var query = 
                _imports.Include(i => i.Items)
                    .Where(i=>i.Items.Any(ii=>ii.ImportId.Equals(invoiceId) && ii.ComponentId.Equals(componentId)))
                    .SelectMany(ii=>ii.Items).FirstOrDefaultAsync(ii=>ii.ImportId.Equals(invoiceId) && ii.ComponentId.Equals(componentId));
            return query;


        }

        public async Task UpdateAsync(ImportModel import)
        {
            _imports.Update(import);
            await context.SaveChangesAsync();
        }

        public Task<double> FindTotalPrice()
        {
            var query = _imports.Include(i => i.Items)
                .SelectMany(ii => ii.Items);
            return query.SumAsync(ii => ii.Quantity * ii.PricePerPiece);
        }

        public Task<double> FindSumForDate(DateOnly date)
        {
            var query = _imports.Include(i => i.Items)
                .Where(i => i.DateIssued.Equals(date))
                .SelectMany(ii => ii.Items);
            return query.SumAsync(ii=>ii.Quantity * ii.PricePerPiece);
        }
    }
}