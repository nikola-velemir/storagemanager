using Microsoft.EntityFrameworkCore;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Domain.Document.Model;
using StoreManager.Domain.MechanicalComponent.Model;
using StoreManager.Infrastructure.Context;

namespace StoreManager.Infrastructure.MechanicalComponent.Repository
{
    public class MechanicalComponentRepository : IMechanicalComponentRepository
    {
        private readonly DbSet<Domain.MechanicalComponent.Model.MechanicalComponent> _components;

        public MechanicalComponentRepository(WarehouseDbContext context)
        {
            _components = context.MechanicalComponents;
        }

        public async Task<int> CountQuantityAsync(Domain.MechanicalComponent.Model.MechanicalComponent component)
        {
            var query = _components
                .Where(mc => mc.Id.Equals(component.Id))
                .SumAsync(mc => mc.CurrentStock);
            return await query;
        }

        public async Task<Domain.MechanicalComponent.Model.MechanicalComponent?> CreateAsync(Domain.MechanicalComponent.Model.MechanicalComponent component)
        {
            var savedInstance = await _components.AddAsync(component);
            return savedInstance.Entity;
        }

        public async Task<Domain.MechanicalComponent.Model.MechanicalComponent> CreateFromExtractionMetadataAsync(ExtractionMetadata metadata)
        {
            var foundComponent = await FindByIdentifierAsync(metadata.Identifier);

            if (foundComponent != null)
            {
                foundComponent.IncreaseStock(metadata.Quantity);
                return foundComponent;
            }

            var component = new Domain.MechanicalComponent.Model.MechanicalComponent
            {
                Id = Guid.NewGuid(), Identifier = metadata.Identifier, Name = metadata.Name,
                CurrentStock = metadata.Quantity
            };

            var savedInstance = await _components.AddAsync(component);
            return savedInstance.Entity;
        }

        public async Task<List<Domain.MechanicalComponent.Model.MechanicalComponent>> CreateFromExtractionMetadataAsync(
            List<ExtractionMetadata> metadata)
        {
            var components = new List<Domain.MechanicalComponent.Model.MechanicalComponent>();
            foreach (var data in metadata)
            {
                components.Add(await CreateFromExtractionMetadataAsync(data));
            }

            return components;
        }

        public Task<List<Domain.MechanicalComponent.Model.MechanicalComponent>> FindAllAsync()
        {
            return _components.Select(c => c).ToListAsync();
        }

        public Task<Domain.MechanicalComponent.Model.MechanicalComponent?> FindByIdAsync(Guid componentGuid)
        {
            return _components.Include(mc => mc.Items).ThenInclude(ii => ii.Import).ThenInclude(i => i.Provider)
                .FirstOrDefaultAsync(mc => mc.Id.Equals(componentGuid));
        }

        public Task<Domain.MechanicalComponent.Model.MechanicalComponent?> FindByIdentifierAsync(string identifier)
        {
            return _components.FirstOrDefaultAsync(mc => mc.Identifier.Equals(identifier));
        }

        public async Task<(ICollection<Domain.MechanicalComponent.Model.MechanicalComponent> Items, int TotalCount)> FindFilteredForProductAsync(
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

        public Task<List<Domain.MechanicalComponent.Model.MechanicalComponent>> FindByInvoiceIdAsync(Guid invoiceId)
        {
            return _components.Include(mc => mc.Items).Where(mc => mc.Items.Any(ii => ii.ImportId.Equals(invoiceId)))
                .ToListAsync();
        }

        public async Task<List<Domain.MechanicalComponent.Model.MechanicalComponent>> FindByProviderIdAsync(Guid id)
        {
            var query = _components
                .Include(mc => mc.Items)
                .ThenInclude(ii => ii.Import)
                .ThenInclude(i => i.Provider).Where(mc => mc.Items.Any(ii => ii.Import.Provider.Id.Equals(id)));
            return await query.ToListAsync();
        }

        public async Task<(ICollection<Domain.MechanicalComponent.Model.MechanicalComponent> Items, int TotalCount)> FindFilteredAsync(
            Guid? providerId,
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
            return _components.SumAsync(i => i.CurrentStock);
        }

        public Task<List<Domain.MechanicalComponent.Model.MechanicalComponent>> FindTopFiveInQuantityAsync()
        {
            return _components
                .OrderByDescending(x => x.CurrentStock)
                .Take(5)
                .ToListAsync();
        }

        public Task<List<Domain.MechanicalComponent.Model.MechanicalComponent>> FindByIdsAsync(List<Guid> componentIds)
        {
            return _components.Where(p => componentIds.Contains(p.Id)).ToListAsync();
        }
    }
}