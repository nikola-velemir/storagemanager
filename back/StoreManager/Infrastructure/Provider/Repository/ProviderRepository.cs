using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
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
            return await _providers.FirstOrDefaultAsync(p => p.Id.Equals(id));
        }
    }
}
