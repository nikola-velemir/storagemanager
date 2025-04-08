using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.MechanicalComponent.Model;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.Provider.Model;
using System.Diagnostics.Eventing.Reader;

namespace StoreManager.Tests.MechanicalComponent.Repository
{
    public class MechanicalComponentRepositoryTest : IAsyncLifetime
    {
        private MechanicalComponentRepository _repository;
        private WarehouseDbContext _context;

        private readonly static string INVALID_IDENTIFIER = "INVALID";
        private readonly static string EXISTING_IDENTIFIER = "MC-400";
        private readonly static string VALID_IDENTIFIER = "MC-100";

        private readonly static MechanicalComponentModel EXISTING_COMPONENT = new MechanicalComponentModel { Id = Guid.NewGuid(), Identifier = EXISTING_IDENTIFIER, Name = "Test" };
        private readonly static MechanicalComponentModel VALID_COMPONENT = new MechanicalComponentModel { Id = Guid.NewGuid(), Identifier = VALID_IDENTIFIER, Name = "Test" };
        private readonly static DocumentModel VALID_DOCUMENT = new DocumentModel
        {
            Type = "pdf",
            ChunkCount = 0,
            Chunks = new List<DocumentChunkModel>(),
            Date = DateOnly.FromDateTime(DateTime.UtcNow),
            FileName = "test",
            Id = Guid.NewGuid()
        };
        private readonly static List<ExtractionMetadata> METADATA_LIST = new List<ExtractionMetadata>()
        {
            new ExtractionMetadata("MC-111", "A",4,4.000),
            new ExtractionMetadata("MC-4001", "B",121,1.3)
        };
        private readonly static ProviderModel VALID_PROVIDER = new ProviderModel
        {
            Adress = "aa",
            Id = Guid.NewGuid(),
            Name = "aaaa",
            PhoneNumber = "sadsadsa",
            Invoices = new List<InvoiceModel>()
        };
        private readonly static InvoiceModel VALID_INVOICE = new InvoiceModel
        {
            DateIssued = DateOnly.FromDateTime(DateTime.UtcNow),
            Document = VALID_DOCUMENT,
            DocumentId = VALID_DOCUMENT.Id,
            Id = Guid.NewGuid(),
            Provider = VALID_PROVIDER,
            ProviderId = VALID_PROVIDER.Id

        };
        private readonly static InvoiceItemModel VALID_INVOICE_ITEM = new InvoiceItemModel
        {
            Component = EXISTING_COMPONENT,
            ComponentId = EXISTING_COMPONENT.Id,
            Invoice = VALID_INVOICE,
            InvoiceId = VALID_INVOICE.Id,
            PricePerPiece = 512321.0,
            Quantity = 10
        };
        [Fact(DisplayName = "Create from extraction data list")]
        public async Task CreateFromExtractionMetadataList_Test()
        {
            var components = await _repository.CreateFromExtractionMetadata(METADATA_LIST);
            Assert.True(components.Count == 2);
            var foundComponets = _context.MechanicalComponents.Where(c => c.Identifier.Equals("MC-111") || c.Identifier.Equals("MC-4001")).ToList();
            Assert.True(foundComponets.Count == 2);

            foreach (var component in components)
            {
                var matchingComponent = foundComponets.FirstOrDefault(c => c.Identifier == component.Identifier);
                Assert.NotNull(matchingComponent);
                Assert.Equal(component.Identifier, matchingComponent.Identifier);
                Assert.Equal(component.Name, matchingComponent.Name);
                Assert.Equal(component.Id, matchingComponent.Id);
            }
        }
        [Fact(DisplayName = "Create from extraction data")]
        public async Task CreateFromExtractionMetadata_NonExistantIdentifier()
        {
            var component = await _repository.CreateFromExtractionMetadata(new ExtractionMetadata("MC-500", "test2", 400, 24.3));
            Assert.NotNull(component);
            var foundComponent = await _context.MechanicalComponents.FirstOrDefaultAsync(c => c.Identifier.Equals("MC-500"));
            Assert.Equal(component, foundComponent);
        }
        [Fact(DisplayName = "Create from extraction data")]
        public async Task CreateFromExtractionMetadata_ExistantIdentifier()
        {
            var component = await _repository.CreateFromExtractionMetadata(new ExtractionMetadata(EXISTING_IDENTIFIER, "test2", 400, 24.3));
            Assert.NotNull(component);
            Assert.Equal(EXISTING_COMPONENT, component);
        }
        [Fact(DisplayName ="Count quantity - invalid model")]
        public async Task CountQuantity_InvalidModel()
        {
            var quantity = await _repository.CountQuantity(VALID_COMPONENT);
            Assert.Equal(0, quantity);
        }
        [Fact(DisplayName = "Count quantity - valid model")]
        public async Task CountQuantity_ValidModel()
        {
            var quantity = await _repository.CountQuantity(EXISTING_COMPONENT);
            Assert.Equal(10, quantity);
        }
        [Fact(DisplayName = "Find by invoice id - invalid id")]
        public async Task FindByInvoiceId_TestInvalidID()
        {
            var components = await _repository.FindByProviderId(Guid.NewGuid());
            Assert.True(components.Count == 0);
        }
        [Fact(DisplayName = "Find by invoice id - valid id")]
        public async Task FindByInvoiceId_TestValidID()
        {
            var components = await _repository.FindByInvoiceId(VALID_INVOICE.Id);
            Assert.True(components.Count == 1);
            Assert.Equal(EXISTING_COMPONENT, components.First());
        }
        [Fact(DisplayName = "Find by provider id - invalid id")]
        public async Task FindByProviderId_TestInvalidID()
        {
            var components = await _repository.FindByProviderId(Guid.NewGuid());
            Assert.True(components.Count == 0);
        }
        [Fact(DisplayName = "Find by provider id - valid id")]
        public async Task FindByProviderId_TestValidID()
        {
            var components = await _repository.FindByProviderId(VALID_PROVIDER.Id);
            Assert.True(components.Count == 1);
            Assert.Equal(EXISTING_COMPONENT, components.First());
        }
        [Fact(DisplayName = "Create test - valid test")]
        public async Task Create()
        {
            var component = await _repository.Create(VALID_COMPONENT);
            Assert.NotNull(component);
            Assert.Equal(VALID_COMPONENT, component);
        }
        [Fact(DisplayName = "Find by identifier test - valid identifier")]
        public async Task FindByIndentifier_TestValidIndentifier()
        {
            var component = await _repository.FindByIdentifier(EXISTING_IDENTIFIER);
            Assert.NotNull(component);
            Assert.Equal(EXISTING_COMPONENT, component);
        }
        [Fact(DisplayName = "Find by identifier test - invalid identifier")]
        public async Task FindByIndentifier_TestInvalidIndentifier()
        {
            var component = await _repository.FindByIdentifier(INVALID_IDENTIFIER);
            Assert.Null(component);
        }
        [Fact(DisplayName = "Find by id - Valid id")]
        public async Task FindById_TestValidId()
        {
            var component = await _repository.FindById(EXISTING_COMPONENT.Id);
            Assert.NotNull(component);
            Assert.Equal(EXISTING_COMPONENT, component);

        }
        [Fact(DisplayName = "Find all")]
        public async Task FindAll_Test()
        {
            var components = await _repository.FindAll();
            Assert.True(components.Count == 1);
            Assert.Equal(components.ElementAt(0), EXISTING_COMPONENT);
        }
        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }
        private async Task SeedTestData()
        {
            VALID_PROVIDER.Invoices.Add(VALID_INVOICE);
            await _context.Providers.AddAsync(VALID_PROVIDER);
            await _context.Invoices.AddAsync(VALID_INVOICE);
            await _context.MechanicalComponents.AddAsync(EXISTING_COMPONENT);
            await _context.InvoiceItems.AddAsync(VALID_INVOICE_ITEM);
            await _context.SaveChangesAsync();
        }
        public async Task InitializeAsync()
        {
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            _context = new WarehouseDbContext(options);
            await SeedTestData();
            _repository = new MechanicalComponentRepository(_context);
        }
    }
}
