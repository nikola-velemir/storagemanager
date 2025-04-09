using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.MechanicalComponent.Model;

namespace StoreManager.Infrastructure.MechanicalComponent.Repository
{
    public class MechanicalComponentRepository : IMechanicalComponentRepository
    {
        private readonly WarehouseDbContext _context;
        private readonly DbSet<MechanicalComponentModel> _components;
        public MechanicalComponentRepository(WarehouseDbContext context)
        {
            _context = context;
            _components = context.MechanicalComponents;
        }

        public async Task<int> CountQuantity(MechanicalComponentModel componentModel)
        {
            var query = _components
                .Where(mc => mc.Id.Equals(componentModel.Id))
                .SelectMany(mc => mc.Items)
                .SumAsync(i => i.Quantity);
            return await query;
        }

        public async Task<MechanicalComponentModel?> Create(MechanicalComponentModel component)
        {
            var savedInstance = await _components.AddAsync(component);
            await _context.SaveChangesAsync();
            return savedInstance.Entity;
        }

        public async Task<MechanicalComponentModel> CreateFromExtractionMetadata(ExtractionMetadata metadata)
        {
            var component = new MechanicalComponentModel { Id = Guid.NewGuid(), Identifier = metadata.Identifier, Name = metadata.Name };

            var foundComponent = await FindByIdentifier(metadata.Identifier);

            if (foundComponent != null) { return foundComponent; }

            var savedInstance = await _components.AddAsync(component);
            await _context.SaveChangesAsync();
            return savedInstance.Entity;
        }

        public async Task<List<MechanicalComponentModel>> CreateFromExtractionMetadata(List<ExtractionMetadata> metadata)
        {
            List<MechanicalComponentModel> components = new();
            foreach (var data in metadata)
            {
                components.Add(await CreateFromExtractionMetadata(data));
            }
            return components;
        }

        public Task<List<MechanicalComponentModel>> FindAll()
        {
            return _components.Select(c => c).ToListAsync();
        }

        public Task<MechanicalComponentModel?> FindById(Guid componentGuid)
        {
            return _components.Include(mc => mc.Items).ThenInclude(ii => ii.Invoice).ThenInclude(i => i.Provider).FirstOrDefaultAsync(mc => mc.Id.Equals(componentGuid));
        }

        public Task<MechanicalComponentModel?> FindByIdentifier(string identifier)
        {
            return _components.FirstOrDefaultAsync(mc => mc.Identifier.Equals(identifier));
        }

        public Task<List<MechanicalComponentModel>> FindByInvoiceId(Guid invoiceId)
        {
            return _components.Include(mc => mc.Items).Where(mc => mc.Items.Any(ii => ii.InvoiceId.Equals(invoiceId))).ToListAsync();
        }

        public async Task<List<MechanicalComponentModel>> FindByProviderId(Guid id)
        {
            var query = _components
                .Include(mc => mc.Items)
                .ThenInclude(ii => ii.Invoice)
                .ThenInclude(i => i.Provider).Where(mc => mc.Items.Any(ii => ii.Invoice.Provider.Id.Equals(id)));
            return await query.ToListAsync();
        }

        public async Task<(ICollection<MechanicalComponentModel> Items, int TotalCount)> FindFiltered(Guid? providerId, string? componentInfo, int pageNumber, int pageSize)
        {
            var query = _components.Include(mc => mc.Items).ThenInclude(ii => ii.Invoice).ThenInclude(i => i.Provider).AsQueryable();
            if (providerId.HasValue)
            {
                query = query.Where(mc => mc.Items.Any(ii => ii.Invoice.Provider.Id == providerId.Value));
            }
            if (!string.IsNullOrEmpty(componentInfo))
            {
                query = query.Where(mc => mc.Name.ToLower().Contains(componentInfo.ToLower()) || mc.Identifier.ToLower().Contains(componentInfo.ToLower()));
            }

            var count = await query.CountAsync();
            int skip = (pageNumber - 1) * pageSize;
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return (items, count);
        }

        public Task<int> FindQuantitySum()
        {
            return _components.Include(mc => mc.Items).SelectMany(mc => mc.Items).Where(i => i.Quantity > 0).SumAsync(i => i.Quantity);

        }
        public Task<List<MechanicalComponentModel>> FindTopFiveInQuantity()
        {

            return _components
                .Include(mc => mc.Items)

                .OrderByDescending(x => x.Items.Sum(i => i.Quantity))
                .Take(5)
                .ToListAsync();
        }
    }
}
