
using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.Invoice.Repository;

namespace StoreManager.Tests.Invoice.Repository
{
    public class InvoiceRepositoryTest : IAsyncLifetime
    {
        private static readonly DocumentModel VALID_DOCUMENT = new DocumentModel { ChunkCount = 0, Chunks = new List<DocumentChunkModel>(), Type = "pdf", Date = DateOnly.FromDateTime(DateTime.UtcNow), FileName = "test", Id = Guid.NewGuid() };
        private static readonly InvoiceModel VALID_INVOICE = new InvoiceModel { DateIssued = DateOnly.FromDateTime(DateTime.UtcNow), Document = VALID_DOCUMENT, DocumentId = VALID_DOCUMENT.Id, Id = Guid.NewGuid(), Items = new List<InvoiceItemModel>() };
        private InvoiceRepository _repository;
        private WarehouseDbContext _context;

        [Fact(DisplayName = "Create test")]
        public async Task Create_Test()
        {
            var response = await _repository.Create(VALID_INVOICE);
            Assert.NotNull(response);
            Assert.Equal(VALID_INVOICE, response);
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
            await _repository.Create(VALID_INVOICE);
            var response = await _repository.FindByDocumentId(VALID_DOCUMENT.Id);
            Assert.NotNull(response);
            Assert.Equal(VALID_INVOICE, response);
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
            _repository = new InvoiceRepository(_context);
        }

        private async Task SeedTestData()
        {
            await _context.Documents.AddAsync(VALID_DOCUMENT);
        }
    }
}
