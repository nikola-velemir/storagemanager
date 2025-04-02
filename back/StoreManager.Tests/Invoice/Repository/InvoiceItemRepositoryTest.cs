
using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.MechanicalComponent.Model;
using System.Threading.Tasks;

namespace StoreManager.Tests.Invoice.Repository
{
    public sealed class InvoiceItemRepositoryTest : IAsyncLifetime
    {
        private InvoiceItemRepository _repository;
        private WarehouseDbContext _context;

        private static readonly MechanicalComponentModel VALID_COMPONENT = new MechanicalComponentModel { Id = Guid.NewGuid(), Identifier = "MC-12321", Name = "Test" };
        private static readonly DocumentModel VALID_DOCUMENT = new DocumentModel { ChunkCount = 0, Chunks = new List<DocumentChunkModel>(), Type = "pdf", Date = DateOnly.FromDateTime(DateTime.UtcNow), FileName = "test", Id = Guid.NewGuid() };
        private static readonly InvoiceModel VALID_INVOICE = new InvoiceModel { DateIssued = DateOnly.FromDateTime(DateTime.UtcNow), Document = VALID_DOCUMENT, DocumentId = VALID_DOCUMENT.Id, Id = Guid.NewGuid(), Items = new List<InvoiceItemModel>() };
        private static readonly InvoiceItemModel VALID_INVOICE_ITEM = new InvoiceItemModel
        {
            Component = VALID_COMPONENT,
            ComponentId = VALID_COMPONENT.Id,
            Invoice = VALID_INVOICE,
            InvoiceId = VALID_INVOICE.Id,
            PricePerPiece = 100.0,
            Quantity = 1
        };

        [Fact(DisplayName = "Create test")]
        public async Task Create_Test()
        {
            var result = await _repository.Create(VALID_INVOICE_ITEM);
            Assert.NotNull(result);
            Assert.Equal(result, VALID_INVOICE_ITEM);
        }
        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        private async Task SeedTestData()
        {
            await _context.Documents.AddAsync(VALID_DOCUMENT);
            await _context.Invoices.AddAsync(VALID_INVOICE);
            await _context.MechanicalComponents.AddAsync(VALID_COMPONENT);
        }

        public async Task InitializeAsync()
        {
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            _context = new WarehouseDbContext(options);
            await SeedTestData();
            _repository = new InvoiceItemRepository(_context);
        }
    }
}
