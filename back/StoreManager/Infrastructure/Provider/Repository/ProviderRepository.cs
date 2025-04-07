using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.Provider.Model;

namespace StoreManager.Infrastructure.Provider.Repository
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly WarehouseDbContext _context;
        private readonly DbSet<ProviderModel> _providers;
        public ProviderRepository(WarehouseDbContext context)
        {
            _context = context;
            _providers = context.Providers;
        }

        public async Task AddInvoice(ProviderModel provider, InvoiceModel invoice)
        {
            provider.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<ProviderModel> Create(ProviderModel provider)
        {
            var savedInstance = await _providers.AddAsync(provider);
            await _context.SaveChangesAsync();
            return savedInstance.Entity;
        }

        public async Task<List<ProviderModel>> FindAll()
        {
            return await _providers.Select(p => p).ToListAsync();
        }

        public async Task<ProviderModel?> FindById(Guid id)
        {
            return await _providers.Include(p => p.Invoices).FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<(ICollection<ProviderModel> Items, int TotalCount)> FindFiltered(string? providerInfo, int pageNumber, int pageSize)
        {
            var query = _providers.Include(p => p.Invoices).AsQueryable();
            if (!string.IsNullOrEmpty(providerInfo))
            {
                query = query.Where(p => 
                p.Name.ToLower().Contains(providerInfo.ToLower()) || 
                p.Adress.ToLower().Contains(providerInfo.ToLower()) || 
                p.PhoneNumber.ToLower().Contains(providerInfo.ToLower()));
            }
            var count = await query.CountAsync();
            int skip = (pageNumber - 1) * pageSize;
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return (items, count);
        }
        public async Task<ProviderModel> Update(ProviderModel provider)
        {
            var savedEntity = _providers.Update(provider);
            await _context.SaveChangesAsync();
            return savedEntity.Entity;
        }
    }
}
