using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using System.Text;
using StoreManager.Application.BusinessPartner.Provider.DTO;
using StoreManager.Application.BusinessPartner.Provider.Repository;
using StoreManager.Application.Document.DTO;
using StoreManager.Application.Document.Repository;
using StoreManager.Application.Document.Service;
using StoreManager.Application.Document.Service.FileService;
using StoreManager.Application.Document.Service.Reader;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Domain.Document.Model;
using StoreManager.Domain.Document.Service;
using StoreManager.Domain.Document.Specification;
using StoreManager.Domain.Document.Storage.Service;
using StoreManager.Infrastructure.Document.Reader;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Invoice.Import.Repository;
using StoreManager.Infrastructure.Invoice.Import.Repository.Specification;
using StoreManager.Infrastructure.Invoice.Import.Service;

namespace StoreManager.Tests.Document.Service
{
    public sealed class DocumentServiceTest : IAsyncLifetime
    {
        private static readonly ProviderModel provider = new ProviderModel
        {
            Address = "aaa", Id = Guid.NewGuid(), Name = "kita", PhoneNumber = "adsa",
            Type = BusinessPartnerType.Provider
        };

        private static readonly ProviderFormDataRequestDto providerFormRequest =
            new ProviderFormDataRequestDto(provider.Id.ToString(), provider.Address, provider.Name,
                provider.PhoneNumber);

        private Mock<IDocumentRepository> _documentRepository;
        private Mock<ICloudStorageService> _supaService;
        private Mock<IImportRepository> _invoiceRepository;
        private Mock<IImportService> _invoiceService;
        private Mock<IDocumentReaderFactory> _readerFactory;
        private Mock<IWebHostEnvironment> _env;
        private Mock<IProviderRepository> _providerRepository;
        private Mock<IFileService> _fileService;
        private DocumentService _service;

        private static readonly string VALID_FILE_NAME = "file";

        private static readonly string INVALID_FILE_NAME = "INVALID";
        private static readonly string VALID_FILE_EXTENSION = "text/plain";
        private static readonly string VALID_FILE_CONTENT = "Hello World!";
        private static readonly Guid VALID_FILE_ID = Guid.NewGuid();

        private static readonly RequestDocumentDownloadResponseDto VALID_DOWNLOAD_REQUEST_RESPONSE =
            new RequestDocumentDownloadResponseDto(VALID_FILE_NAME, "text/plain", 1);

        private static readonly DocumentChunkModel VALID_CHUNK = new DocumentChunkModel
        {
            Id = Guid.NewGuid(),
            DocumentId = VALID_FILE_ID,
            ChunkNumber = 0,
            SupaBasePath = "your/path/here",
            Document = null
        };

        private static readonly DocumentModel VALID_DOCUMENT = new DocumentModel
        {
            Id = VALID_FILE_ID,
            Type = VALID_FILE_EXTENSION,
            Chunks = new List<DocumentChunkModel>
            {
                VALID_CHUNK
            },
            Date = DateOnly.FromDateTime(DateTime.UtcNow),
            FileName = VALID_FILE_NAME
        };

        private static readonly ImportModel VALID_IMPORT = new ImportModel
        {
            ProviderId = provider.Id,
            Provider = provider,
            DateIssued = VALID_DOCUMENT.Date,
            Document = VALID_DOCUMENT,
            DocumentId = VALID_DOCUMENT.Id,
            Id = Guid.NewGuid()
        };

        private static readonly IFormFile VALID_FILE = GenerateValidMockFile();

        private static readonly DocumentDownloadResponseDto VALID_RESPONSE =
            new DocumentDownloadResponseDto(Encoding.UTF8.GetBytes(VALID_FILE_CONTENT), VALID_FILE_EXTENSION);

        [Fact(DisplayName = "Download chunk - invalid file name")]
        public async Task DownloadChunk_InvalidFileNameTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var response = await _service.DownloadChunk(INVALID_FILE_NAME, 0);
            });
            Assert.NotNull(exception);
            Assert.Equal("Guid cannot be parsed", exception.Message);
            Assert.IsType<InvalidCastException>(exception);

            _documentRepository.Verify(repo => repo.FindByNameAsync( new DocumentWithDocumentChunks(),INVALID_FILE_NAME), Times.Never);
            _supaService.Verify(supa => supa.DownloadChunk(It.IsAny<DocumentChunkModel>()), Times.Never);
        }

        [Fact(DisplayName = "Download chunk - invalid chunk index")]
        public async Task DownloadChunk_InvalidChunkIndexTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var response = await _service.DownloadChunk(VALID_IMPORT.Id.ToString(), 52);
            });
            Assert.NotNull(exception);
            Assert.Equal("Chunk not found", exception.Message);
            Assert.IsType<EntryPointNotFoundException>(exception);

            _documentRepository.Verify(repo => repo.FindByNameAsync(new DocumentWithDocumentChunks(),VALID_FILE_NAME), Times.Once);
            _supaService.Verify(supa => supa.DownloadChunk(It.IsAny<DocumentChunkModel>()), Times.Never);
        }

        [Fact(DisplayName = "Download chunk - valid test")]
        public async Task DownloadChunk_ValidTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var response = await _service.DownloadChunk(VALID_IMPORT.Id.ToString(), 0);
                Assert.NotNull(response);
                Assert.Equal(VALID_RESPONSE, response);
            });
            Assert.Null(exception);


            _documentRepository.Verify(repo => repo.FindByNameAsync(new DocumentWithDocumentChunks(),VALID_FILE_NAME), Times.Once);
            _supaService.Verify(supa => supa.DownloadChunk(It.IsAny<DocumentChunkModel>()), Times.Once);
        }

        [Fact(DisplayName = "Upload chunk - valid test")]
        public async Task UploadChunk_ValidTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                await _service.UploadChunk(JsonConvert.SerializeObject(providerFormRequest),
                    GenerateValidMockFile(), $"{VALID_FILE_NAME}", 0, VALID_DOCUMENT.Chunks.Count);
            });
            Assert.Null(exception);


            _documentRepository.Verify(repo => repo.FindByNameAsync(new DocumentWithDocumentChunks(),VALID_FILE_NAME), Times.Once);
            _documentRepository.Verify(repo => repo.SaveFileAsync(VALID_FILE_NAME), Times.Never);
            _invoiceRepository.Verify(repo => repo.Create(VALID_IMPORT), Times.Never);
            _supaService.Verify(supa => supa.UploadFileChunk(It.IsAny<IFormFile>(), It.IsAny<DocumentChunkModel>()),
                Times.Once);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        private void MockService()
        {
            _supaService.Setup(supa => supa.DownloadChunk(It.IsAny<DocumentChunkModel>()))
                .ReturnsAsync(VALID_RESPONSE.bytes);
            _supaService.Setup(supa => supa.UploadFileChunk(VALID_FILE, VALID_CHUNK)).ReturnsAsync(string.Empty);
            _invoiceService.Setup(s => s.Create(It.IsAny<Guid>(), It.IsAny<List<ExtractionMetadata>>()))
                .Returns(Task.CompletedTask);
            _fileService.Setup(s => s.DeleteAllChunks(It.IsAny<DocumentModel>())).Returns(Task.CompletedTask);
            _fileService.Setup(s => s.AppendChunk(It.IsAny<IFormFile>(), It.IsAny<DocumentModel>()))
                .Returns(Task.CompletedTask);
            //  _supaService.Setup(supa=>supa.)
        }

        private Task MockFactory()
        {
            var readerService = new Mock<PdfService>();
            readerService.Setup(rs => rs.ExtractDataFromDocument(It.IsAny<string>()))
                .Returns(new List<ExtractionMetadata>());
            _readerFactory.Setup(f => f.GetReader(It.IsAny<string>())).Returns(readerService.Object);
            return Task.CompletedTask;
        }

        private Task MockRepository()
        {
            _documentRepository.Setup(repo => repo.FindByNameAsync(new DocumentWithDocumentChunks(),VALID_FILE_NAME)).ReturnsAsync(VALID_DOCUMENT);
            _documentRepository.Setup(repo => repo.FindByNameAsync(new DocumentWithDocumentChunks(),INVALID_FILE_NAME)).ReturnsAsync((DocumentModel?)null);

            _documentRepository.Setup(repo => repo.SaveFileAsync(VALID_FILE_NAME)).ReturnsAsync(VALID_DOCUMENT);
            _documentRepository.Setup(repo => repo.SaveChunkAsync(VALID_FILE, VALID_FILE_NAME, 0)).ReturnsAsync(VALID_CHUNK);
            _invoiceRepository.Setup(repo => repo.Create(VALID_IMPORT)).ReturnsAsync(VALID_IMPORT);
            _invoiceRepository.Setup(repo => repo.FindById(new ImportWithDocument(),VALID_IMPORT.Id)).ReturnsAsync(VALID_IMPORT);

            _providerRepository.Setup(repo => repo.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(provider);
            _providerRepository.Setup(repo => repo.CreateAsync(It.IsAny<ProviderModel>())).ReturnsAsync(provider);
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            VALID_IMPORT.Document = VALID_DOCUMENT;

            _documentRepository = new Mock<IDocumentRepository>();
            _supaService = new Mock<ICloudStorageService>();
            _invoiceRepository = new Mock<IImportRepository>();
            _invoiceService = new Mock<IImportService>();
            _readerFactory = new Mock<IDocumentReaderFactory>();
            _env = new Mock<IWebHostEnvironment>();
            _providerRepository = new Mock<IProviderRepository>();
            _fileService = new Mock<IFileService>();
            await MockRepository();
            MockService();
            await MockFactory();
            _env.Setup(env => env.WebRootPath)
                .Returns("C:\\FakeRoot");
            _service = new DocumentService(_invoiceService.Object, _documentRepository.Object, _supaService.Object,
                _invoiceRepository.Object, _env.Object, _readerFactory.Object, _providerRepository.Object,
                _fileService.Object);
        }

        public static IFormFile GenerateValidMockFile()
        {
            var fileName = $"{VALID_FILE_NAME}.{VALID_FILE_EXTENSION}";
            var content = "Hello, World!";
            var contentBytes = Encoding.UTF8.GetBytes(content);
            var stream = new MemoryStream(contentBytes);
            var formFile = new FormFile(stream, 0, stream.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };
            return formFile;
        }
    }
}