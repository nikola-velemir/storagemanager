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

        public Task<MechanicalComponentModel?> FindByIdentifier(string identifier)
        {
            return _components.FirstOrDefaultAsync(mc => mc.Identifier.Equals(identifier));
        }

        public async Task<(ICollection<MechanicalComponentModel> Items, int TotalCount)> FindFiltered(int pageNumber, int pageSize)
        {
            var query = _components.AsQueryable();
            var count = await query.CountAsync();
            int skip = (pageNumber - 1) * pageSize;
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return (items, count);
        }
    }
}
