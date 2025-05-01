using Moq;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Application.Invoice.Import.Service;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Domain.Document.Model;
using StoreManager.Domain.MechanicalComponent.Model;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Invoice.Import.Repository;

namespace StoreManager.Tests.Invoice.Service
{
    public class ImportServiceTest : IAsyncLifetime
    {
        private ImportService _service;
        private Mock<IImportItemRepository> _invoiceItemRepository;
        private Mock<IMechanicalComponentRepository> _mechanicalComponentRepository;
        private Mock<IImportRepository> _invoiceRepository;

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

        private static readonly Guid INVALID_GUID = Guid.NewGuid();

        private readonly static List<ExtractionMetadata> METADATA_LIST = new List<ExtractionMetadata>()
        {
            new ExtractionMetadata("MC-111", "A", 4, 4.000),
            new ExtractionMetadata("MC-4001", "B", 121, 1.3)
        };

        private readonly static List<Domain.MechanicalComponent.Model.MechanicalComponent> COMPONENTS = new List<Domain.MechanicalComponent.Model.MechanicalComponent>()
        {
            new Domain.MechanicalComponent.Model.MechanicalComponent { Id = Guid.NewGuid(), Identifier = "MC-111", Name = "Test" },
            new Domain.MechanicalComponent.Model.MechanicalComponent { Id = Guid.NewGuid(), Identifier = "MC-4001", Name = "Test" }
        };

        [Fact(DisplayName = "Create test - Invalid document id")]
        public async Task CreateTest_InvalidDocumentId()
        {
            await _service.Create(INVALID_GUID, METADATA_LIST);
            _invoiceRepository.Verify(repo => repo.FindByDocumentId(INVALID_GUID), Times.Once);
            _mechanicalComponentRepository.Verify(repo => repo.FindByIdentifierAsync(It.IsAny<string>()), Times.Never);
            _mechanicalComponentRepository.Verify(
                repo => repo.CreateFromExtractionMetadataAsync(It.IsAny<List<ExtractionMetadata>>()), Times.Never);
            _invoiceItemRepository.Verify(repo => repo.Create(It.IsAny<ImportItemModel>()), Times.Never);
        }

        [Fact(DisplayName = "Create test - Invalid document id")]
        public async Task CreateTest_VALID()
        {
            await _service.Create(VALID_DOCUMENT.Id, METADATA_LIST);

            _invoiceRepository.Verify(repo => repo.FindByDocumentId(VALID_DOCUMENT.Id), Times.Once);

            _mechanicalComponentRepository.Verify(repo => repo.FindByIdentifierAsync(It.IsAny<string>()), Times.Exactly(2));
            _mechanicalComponentRepository.Verify(repo => repo.FindByIdentifierAsync("MC-111"), Times.Once);
            _mechanicalComponentRepository.Verify(repo => repo.FindByIdentifierAsync("MC-4001"), Times.Once);
            _mechanicalComponentRepository.Verify(
                repo => repo.CreateFromExtractionMetadataAsync(It.IsAny<List<ExtractionMetadata>>()), Times.Once);

            _invoiceItemRepository.Verify(repo => repo.Create(It.IsAny<ImportItemModel>()), Times.Exactly(2));
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        private void MockRepository()
        {
            _invoiceRepository.Setup(repo => repo.FindByDocumentId(VALID_DOCUMENT.Id)).ReturnsAsync(VALID_IMPORT);
            _invoiceRepository.Setup(repo => repo.FindByDocumentId(INVALID_GUID)).ReturnsAsync((Import?)null);
            _mechanicalComponentRepository.Setup(repo => repo.CreateFromExtractionMetadataAsync(METADATA_LIST))
                .ReturnsAsync(COMPONENTS);
            _mechanicalComponentRepository.Setup(repo => repo.FindByIdentifierAsync("MC-111"))
                .ReturnsAsync(COMPONENTS.Find(c => c.Identifier.Equals("MC-111")));
            _mechanicalComponentRepository.Setup(repo => repo.FindByIdentifierAsync("MC-4001"))
                .ReturnsAsync(COMPONENTS.Find(c => c.Identifier.Equals("MC-4001")));
        }

        public Task InitializeAsync()
        {
            _invoiceItemRepository = new Mock<IImportItemRepository>();
            _mechanicalComponentRepository = new Mock<IMechanicalComponentRepository>();
            _invoiceRepository = new Mock<IImportRepository>();
            MockRepository();
            _service = new ImportService(_invoiceItemRepository.Object, _mechanicalComponentRepository.Object,
                _invoiceRepository.Object);
            return Task.CompletedTask;
        }
    }
}