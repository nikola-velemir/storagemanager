
using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.MechanicalComponent.Model;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using System.Threading.Tasks;

namespace StoreManager.Tests.MechanicalComponent
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
        private readonly static List<ExtractionMetadata> METADATA_LIST = new List<ExtractionMetadata>()
        {
            new ExtractionMetadata("MC-111", "A",4,4.000),
            new ExtractionMetadata("MC-4001", "B",121,1.3)
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
                Assert.NotNull(matchingComponent); // Ensure component is found
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
        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }
        private async Task SeedTestData()
        {
            await _context.MechanicalComponents.AddAsync(EXISTING_COMPONENT);
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
