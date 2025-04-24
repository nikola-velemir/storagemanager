using Microsoft.EntityFrameworkCore;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Domain.Document.Model;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Invoice.Import.Repository;

namespace StoreManager.Tests.Invoice.Repository
{
    public class ImportRepositoryTest : IAsyncLifetime
    {
        private static readonly Provider provider = new Provider
        {
            Address = "aaa", Type = BusinessPartnerType.Provider, Id = Guid.NewGuid(), Name = "kita",
            PhoneNumber = "adsa"
        };

        private static readonly Domain.Document.Model.Document VALID_DOCUMENT = new Domain.Document.Model.Document
        {
            Chunks = new List<DocumentChunk>(), Type = "pdf",
            Date = DateOnly.FromDateTime(DateTime.UtcNow), FileName = "test", Id = Guid.NewGuid()
        };

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

        private ImportRepository _repository;
        private WarehouseDbContext _context;

        [Fact(DisplayName = "Create test")]
        public async Task Create_Test()
        {
            var response = await _repository.Create(VALID_IMPORT);
            Assert.NotNull(response);
            Assert.Equal(VALID_IMPORT, response);
        }

        [Fact(DisplayName = "Find by document id - invalid document id")]
        public async Task FindByDocumentId_InvalidIdTest()
        {
            var response = await _repository.FindByDocumentId(Guid.NewGuid());
            Assert.Null(response);
        }

        [Fact(DisplayName = "Find by document id - valid document id")]
        public async Task FindByDocumentId_ValidIdTest()
        {
            await _repository.Create(VALID_IMPORT);
            var response = await _repository.FindByDocumentId(VALID_DOCUMENT.Id);
            Assert.NotNull(response);
            Assert.Equal(VALID_IMPORT, response);
        }

        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new WarehouseDbContext(options);
            await SeedTestData();
            _repository = new ImportRepository(_context);
        }

        private async Task SeedTestData()
        {
            await _context.Documents.AddAsync(VALID_DOCUMENT);
        }
    }
}