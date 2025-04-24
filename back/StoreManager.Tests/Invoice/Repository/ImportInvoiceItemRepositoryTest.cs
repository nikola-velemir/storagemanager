
using Microsoft.EntityFrameworkCore;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Infrastructure.DB;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Domain.Document.Model;
using StoreManager.Domain.MechanicalComponent.Model;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Invoice.Import.Repository;

namespace StoreManager.Tests.Invoice.Repository
{
    public sealed class ImportItemRepositoryTest : IAsyncLifetime
    {
        private ImportItemRepository _repository;
        private WarehouseDbContext _context;
        private static readonly Provider provider = new Provider {Type = BusinessPartnerType.Provider, Address = "aaa", Id = Guid.NewGuid(), Name = "kita", PhoneNumber = "adsa" };

        private static readonly Domain.MechanicalComponent.Model.MechanicalComponent VALID_COMPONENT = new Domain.MechanicalComponent.Model.MechanicalComponent { Id = Guid.NewGuid(), Identifier = "MC-12321", Name = "Test" };
        private static readonly Domain.Document.Model.Document VALID_DOCUMENT = new Domain.Document.Model.Document { Chunks = new List<DocumentChunk>(), Type = "pdf", Date = DateOnly.FromDateTime(DateTime.UtcNow), FileName = "test", Id = Guid.NewGuid() };
        private static readonly Import VALID_IMPORT = new Import
        {
            Provider = provider,
            ProviderId = provider.Id,
            DateIssued = DateOnly.FromDateTime(DateTime.UtcNow),
            Document = VALID_DOCUMENT,
            DocumentId = VALID_DOCUMENT.Id,
            Id = Guid.NewGuid(),
            Items = new List<ImportItemModel>()
        };
        private static readonly ImportItemModel VALID_IMPORT_ITEM = new ImportItemModel
        {
            Component = VALID_COMPONENT,
            ComponentId = VALID_COMPONENT.Id,
            Import = VALID_IMPORT,
            ImportId = VALID_IMPORT.Id,
            PricePerPiece = 100.0,
            Quantity = 1
        };

        [Fact(DisplayName = "Create test")]
        public async Task Create_Test()
        {
            var result = await _repository.Create(VALID_IMPORT_ITEM);
            Assert.NotNull(result);
            Assert.Equal(result, VALID_IMPORT_ITEM);
        }
        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        private async Task SeedTestData()
        {
            await _context.Documents.AddAsync(VALID_DOCUMENT);
            await _context.Imports.AddAsync(VALID_IMPORT);
            await _context.MechanicalComponents.AddAsync(VALID_COMPONENT);
        }

        public async Task InitializeAsync()
        {
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            _context = new WarehouseDbContext(options);
            await SeedTestData();
            _repository = new ImportItemRepository(_context);
        }
    }
}
