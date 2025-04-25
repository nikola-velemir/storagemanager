using Microsoft.EntityFrameworkCore;
using StoreManager.Application.BusinessPartner.Provider.Repository;
using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Infrastructure.BusinessPartner.Provider.Repository
{
    public class ProviderRepository : IProviderRepository
    {
        private DbSet<Domain.BusinessPartner.Provider.Model.Provider> _providers;

        public ProviderRepository(WarehouseDbContext context)
        {
            _providers = context.Providers;
        }

        public Task AddInvoiceAsync(Domain.BusinessPartner.Provider.Model.Provider provider, Import import)
        {
            provider.Imports.Add(import);
            return Task.CompletedTask;
        }

        public async Task<Domain.BusinessPartner.Provider.Model.Provider> CreateAsync(
            Domain.BusinessPartner.Provider.Model.Provider provider)
        {
            var savedInstance = await _providers.AddAsync(provider);
            return savedInstance.Entity;
        }

        public async Task<List<Domain.BusinessPartner.Provider.Model.Provider>> FindAllAsync()
        {
            return await _providers.Select(p => p).ToListAsync();
        }

        public async Task<Domain.BusinessPartner.Provider.Model.Provider?> FindByIdAsync(Guid id)
        {
            return await _providers.Include(p => p.Imports).FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<(ICollection<Domain.BusinessPartner.Provider.Model.Provider> Items, int TotalCount)>
            FindFilteredAsync(string? providerInfo, int pageNumber, int pageSize)
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

        public async Task<int> FindInvoiceCountForProviderAsync(Domain.BusinessPartner.Provider.Model.Provider provider)
        {
            var query = await _providers.Where(p => p.Id.Equals(provider.Id)).Include(p => p.Imports)
                .FirstOrDefaultAsync();
            return query?.Imports.Count ?? 0;
        }

        public async Task<int> FindComponentCountForProviderAsync(
            Domain.BusinessPartner.Provider.Model.Provider provider)
        {
            var query = await _providers.Where(p => p.Id.Equals(provider.Id)).Include(p => p.Imports)
                .ThenInclude(i => i.Items).ThenInclude(ii => ii.Component).FirstOrDefaultAsync();
            if (query is null) return 0;

            var components = query.Imports.SelectMany(i => i.Items).Sum(ii => ii.Quantity);
            return components;
        }

        public  Task<Domain.BusinessPartner.Provider.Model.Provider> UpdateAsync(
            Domain.BusinessPartner.Provider.Model.Provider provider)
        {
            var savedEntity = _providers.Update(provider);
            return Task.FromResult(savedEntity.Entity);
        }
    }
}