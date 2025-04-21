using Microsoft.EntityFrameworkCore;
using StoreManager.Application.BusinessPartner.Provider.Repository;
using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Infrastructure.BusinessPartner.Provider.Repository
{
    public class ProviderRepository(WarehouseDbContext context) : IProviderRepository
    {
        private readonly DbSet<ProviderModel> _providers = context.Providers;

        public async Task AddInvoiceAsync(ProviderModel provider, ImportModel import)
        {
            provider.Imports.Add(import);
            await context.SaveChangesAsync();
        }

        public async Task<ProviderModel> CreateAsync(ProviderModel provider)
        {
            var savedInstance = await _providers.AddAsync(provider);
            await context.SaveChangesAsync();
            return savedInstance.Entity;
        }

        public async Task<List<ProviderModel>> FindAllAsync()
        {
            return await _providers.Select(p => p).ToListAsync();
        }

        public async Task<ProviderModel?> FindByIdAsync(Guid id)
        {
            return await _providers.Include(p => p.Imports).FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<(ICollection<ProviderModel> Items, int TotalCount)> FindFilteredAsync(string? providerInfo, int pageNumber, int pageSize)
        {
            var query = _providers.Include(p => p.Imports).AsQueryable();
            if (!string.IsNullOrEmpty(providerInfo))
            {
                query = query.Where(e =>
                    e.Name.ToLower().Contains(providerInfo.ToLower()) ||
                    e.PhoneNumber.ToLower().Contains(providerInfo.ToLower()) ||
                    e.Address.Street.ToLower().Contains(providerInfo.ToLower()) || 
                    e.Address.City.ToLower().Contains(providerInfo.ToLower()) ||
                    e.Address.StreetNumber.ToLower().Contains(providerInfo.ToLower()));
            }
            var count = await query.CountAsync();
            int skip = (pageNumber - 1) * pageSize;
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return (items, count);
        }

        public async Task<int> FindInvoiceCountForProviderAsync(ProviderModel provider)
        {
            var query = await _providers.Where(p => p.Id.Equals(provider.Id)).Include(p => p.Imports)
                .FirstOrDefaultAsync();
            return query?.Imports.Count ?? 0;
        }

        public async Task<int> FindComponentCountForProviderAsync(ProviderModel provider)
        {
            var query = await _providers.Where(p => p.Id.Equals(provider.Id)).Include(p => p.Imports).ThenInclude(i => i.Items).ThenInclude(ii => ii.Component).FirstOrDefaultAsync();
            if (query is null) return 0;

            var components = query.Imports.SelectMany(i => i.Items).Sum(ii => ii.Quantity);
            return components;
        }

        public async Task<ProviderModel> UpdateAsync(ProviderModel provider)
        {
            var savedEntity = _providers.Update(provider);
            await context.SaveChangesAsync();
            return savedEntity.Entity;
        }
    }
}
