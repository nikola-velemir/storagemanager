using Microsoft.EntityFrameworkCore;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Domain.Document.Model;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.MechanicalComponent.Model;

namespace StoreManager.Infrastructure.MechanicalComponent.Repository
{
    public class MechanicalComponentRepository(WarehouseDbContext context) : IMechanicalComponentRepository
    {
        private readonly DbSet<MechanicalComponentModel> _components = context.MechanicalComponents;

        public async Task<int> CountQuantityAsync(MechanicalComponentModel componentModel)
        {
            var query = _components
                .Where(mc => mc.Id.Equals(componentModel.Id))
                .SelectMany(mc => mc.Items)
                .SumAsync(i => i.Quantity);
            return await query;
        }

        public async Task<MechanicalComponentModel?> CreateAsync(MechanicalComponentModel component)
        {
            var savedInstance = await _components.AddAsync(component);
            await context.SaveChangesAsync();
            return savedInstance.Entity;
        }

        public async Task<MechanicalComponentModel> CreateFromExtractionMetadataAsync(ExtractionMetadata metadata)
        {
            var component = new MechanicalComponentModel
                { Id = Guid.NewGuid(), Identifier = metadata.Identifier, Name = metadata.Name };

            var foundComponent = await FindByIdentifierAsync(metadata.Identifier);

            if (foundComponent != null)
            {
                return foundComponent;
            }

            var savedInstance = await _components.AddAsync(component);
            await context.SaveChangesAsync();
            return savedInstance.Entity;
        }

        public async Task<List<MechanicalComponentModel>> CreateFromExtractionMetadataAsync(
            List<ExtractionMetadata> metadata)
        {
            List<MechanicalComponentModel> components = new();
            foreach (var data in metadata)
            {
                components.Add(await CreateFromExtractionMetadataAsync(data));
            }

            return components;
        }

        public Task<List<MechanicalComponentModel>> FindAllAsync()
        {
            return _components.Select(c => c).ToListAsync();
        }

        public Task<MechanicalComponentModel?> FindByIdAsync(Guid componentGuid)
        {
            return _components.Include(mc => mc.Items).ThenInclude(ii => ii.Import).ThenInclude(i => i.Provider)
                .FirstOrDefaultAsync(mc => mc.Id.Equals(componentGuid));
        }

        public Task<MechanicalComponentModel?> FindByIdentifierAsync(string identifier)
        {
            return _components.FirstOrDefaultAsync(mc => mc.Identifier.Equals(identifier));
        }

        public async Task<(ICollection<MechanicalComponentModel> Items, int TotalCount)> FindFilteredForProductAsync(
            Guid? providerId, string? componentInfo, int pageNumber, int pageSize)
        {
            var query = _components.Include(mc => mc.Items).AsQueryable();
            if (providerId.HasValue)
            {
                query = query.Where(mc => mc.Items.Any(ii => ii.Import.Provider.Id == providerId.Value));
            }

            if (!string.IsNullOrEmpty(componentInfo))
            {
                query = query.Where(mc =>
                    mc.Name.ToLower().Contains(componentInfo.ToLower()) ||
                    mc.Identifier.ToLower().Contains(componentInfo.ToLower()));
            }

            var count = await query.CountAsync();
            int skip = (pageNumber - 1) * pageSize;
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return (items, count);
        }

        public Task<List<MechanicalComponentModel>> FindByInvoiceIdAsync(Guid invoiceId)
        {
            return _components.Include(mc => mc.Items).Where(mc => mc.Items.Any(ii => ii.ImportId.Equals(invoiceId)))
                .ToListAsync();
        }

        public async Task<List<MechanicalComponentModel>> FindByProviderIdAsync(Guid id)
        {
            var query = _components
                .Include(mc => mc.Items)
                .ThenInclude(ii => ii.Import)
                .ThenInclude(i => i.Provider).Where(mc => mc.Items.Any(ii => ii.Import.Provider.Id.Equals(id)));
            return await query.ToListAsync();
        }

        public async Task<(ICollection<MechanicalComponentModel> Items, int TotalCount)> FindFilteredAsync(Guid? providerId,
            string? componentInfo, int pageNumber, int pageSize)
        {
            var query = _components.Include(mc => mc.Items).ThenInclude(ii => ii.Import).ThenInclude(i => i.Provider)
                .AsQueryable();
            if (providerId.HasValue)
            {
                query = query.Where(mc => mc.Items.Any(ii => ii.Import.Provider.Id == providerId.Value));
            }

            if (!string.IsNullOrEmpty(componentInfo))
            {
                query = query.Where(mc =>
                    mc.Name.ToLower().Contains(componentInfo.ToLower()) ||
                    mc.Identifier.ToLower().Contains(componentInfo.ToLower()));
            }

            var count = await query.CountAsync();
            int skip = (pageNumber - 1) * pageSize;
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return (items, count);
        }

        public Task<int> FindQuantitySumAsync()
        {
            return _components.Include(mc => mc.Items).SelectMany(mc => mc.Items).Where(i => i.Quantity > 0)
                .SumAsync(i => i.Quantity);
        }

        public Task<List<MechanicalComponentModel>> FindTopFiveInQuantityAsync()
        {
            return _components
                .Include(mc => mc.Items)
                .OrderByDescending(x => x.Items.Sum(i => i.Quantity))
                .Take(5)
                .ToListAsync();
        }

        public Task<List<MechanicalComponentModel>> FindByIdsAsync(List<Guid> componentIds)
        {
            return _components.Where(p => componentIds.Contains(p.Id)).ToListAsync();
        }
    }
}