using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Moq;
using Newtonsoft.Json;
using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.Service;
using StoreManager.Infrastructure.Document.Service.Reader;
using StoreManager.Infrastructure.Document.SupaBase.Service;
using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.Invoice.Service;
using StoreManager.Infrastructure.Provider.DTO;
using StoreManager.Infrastructure.Provider.Model;
using StoreManager.Infrastructure.Provider.Repository;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.Tests.Document.Service
{
    public sealed class DocumentServiceTest : IAsyncLifetime
    {
        private static readonly ProviderModel provider = new ProviderModel { Adress = "aaa", Id = Guid.NewGuid(), Name = "kita", PhoneNumber = "adsa" };
        private static readonly ProviderFormDataRequestDTO providerFormRequest = new ProviderFormDataRequestDTO(provider.Id.ToString(), provider.Adress, provider.Name, provider.PhoneNumber);
        private Mock<IDocumentRepository> _documentRepository;
        private Mock<ICloudStorageService> _supaService;
        private Mock<IInvoiceRepository> _invoiceRepository;
        private Mock<IInvoiceService> _invoiceService;
        private Mock<IDocumentReaderFactory> _readerFactory;
        private Mock<IWebHostEnvironment> _env;
        private Mock<IProviderRepository> _providerRepository;
        private DocumentService _service;

        private static readonly string VALID_FILE_NAME = "file";

        private static readonly string INVALID_FILE_NAME = "INVALID";
        private static readonly string VALID_FILE_EXTENSION = "text/plain";
        private static readonly string VALID_FILE_CONTENT = "Hello World!";
        private static readonly Guid VALID_FILE_ID = Guid.NewGuid();
        private static readonly RequestDocumentDownloadResponseDTO VALID_DOWNLOAD_REQUEST_RESPONSE = new RequestDocumentDownloadResponseDTO(VALID_FILE_NAME, "text/plain", 1);
        private static readonly DocumentChunkModel VALID_CHUNK = new DocumentChunkModel
        {
            Id = Guid.NewGuid(),
            DocumentId = VALID_FILE_ID,
            ChunkNumber = 0,
            SupaBasePath = "your/path/here",
            Document = null!
        };
        private static readonly DocumentModel VALID_DOCUMENT = new DocumentModel
        {
            ChunkCount = 0,
            Id = VALID_FILE_ID,
            Type = VALID_FILE_EXTENSION,
            Chunks = new List<DocumentChunkModel>
            {
                VALID_CHUNK
            },
            Date = DateOnly.FromDateTime(DateTime.UtcNow),
            FileName = VALID_FILE_NAME
        };
        private static readonly InvoiceModel VALID_INVOICE = new InvoiceModel
        {
            ProviderId = provider.Id,
            Provider = provider,
            DateIssued = VALID_DOCUMENT.Date,
            Document = VALID_DOCUMENT,
            DocumentId = VALID_DOCUMENT.Id,
            Id = Guid.NewGuid()
        };
        private static readonly IFormFile VALID_FILE = GenerateValidMockFile();
        private static readonly DocumentDownloadResponseDTO VALID_RESPONSE = new DocumentDownloadResponseDTO(Encoding.UTF8.GetBytes(VALID_FILE_CONTENT), VALID_FILE_EXTENSION);
        [Fact(DisplayName = "Request download - invalid file name")]
        public async Task RequestDownload_InvalidFileNameTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var result = await _service.RequestDownload(INVALID_FILE_NAME);
            });
            Assert.NotNull(exception);

            _documentRepository.Verify(repo => repo.FindByName(INVALID_FILE_NAME), Times.Once);
        }
        [Fact(DisplayName = "Request download - valid file name")]
        public async Task RequestDownload_ValidFileNameTest()
        {
            var documentService = new Mock<DocumentService>(_invoiceService.Object, _documentRepository.Object, _supaService.Object, _invoiceRepository.Object, _env.Object, _readerFactory.Object,_providerRepository.Object);
            _service = documentService.Object;
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var result = await _service.RequestDownload(VALID_FILE_NAME);
                Assert.NotNull(result);
                Assert.Equal(VALID_DOWNLOAD_REQUEST_RESPONSE, result);
            });
            Assert.Null(exception);

            _documentRepository.Verify(repo => repo.FindByName(VALID_FILE_NAME), Times.Once);
        }
        [Fact(DisplayName = "Download chunk - invalid file name")]
        public async Task DownloadChunk_InvalidFileNameTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var response = await _service.DownloadChunk(INVALID_FILE_NAME, 0);

            });
            Assert.NotNull(exception);
            Assert.Equal("File not found", exception.Message);
            Assert.IsType<FileNotFoundException>(exception);

            _documentRepository.Verify(repo => repo.FindByName(INVALID_FILE_NAME), Times.Once);
            _supaService.Verify(supa => supa.DownloadChunk(It.IsAny<DocumentChunkModel>()), Times.Never);
        }
        [Fact(DisplayName = "Download chunk - invalid chunk index")]
        public async Task DownloadChunk_InvalidChunkIndexTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var response = await _service.DownloadChunk(VALID_FILE_NAME, 52);

            });
            Assert.NotNull(exception);
            Assert.Equal("Chunk not found", exception.Message);
            Assert.IsType<EntryPointNotFoundException>(exception);

            _documentRepository.Verify(repo => repo.FindByName(VALID_FILE_NAME), Times.Once);
            _supaService.Verify(supa => supa.DownloadChunk(It.IsAny<DocumentChunkModel>()), Times.Never);
        }
        [Fact(DisplayName = "Download chunk - valid test")]
        public async Task DownloadChunk_ValidTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var response = await _service.DownloadChunk(VALID_FILE_NAME, 0);
                Assert.NotNull(response);
                Assert.Equal(VALID_RESPONSE, response);
            });
            Assert.Null(exception);


            _documentRepository.Verify(repo => repo.FindByName(VALID_FILE_NAME), Times.Once);
            _supaService.Verify(supa => supa.DownloadChunk(It.IsAny<DocumentChunkModel>()), Times.Once);
        }

        [Fact(DisplayName = "Upload chunk - valid test")]
        public async Task UploadChunk_ValidTest()
        {

            var serviceMock = new Mock<DocumentService>(_invoiceService.Object, _documentRepository.Object, _supaService.Object, _invoiceRepository.Object, _env.Object, _readerFactory.Object,_providerRepository.Object);
            //      public DocumentService(IInvoiceService invoiceService, IDocumentRepository repository, ICloudStorageService supabase, IInvoiceRepository invoiceRepository, IWebHostEnvironment env)

            serviceMock.Setup(s => s.AppendChunk(It.IsAny<IFormFile>(), It.IsAny<DocumentModel>())).Returns(Task.CompletedTask);
            serviceMock.Setup(s => s.DeleteAllChunks(It.IsAny<DocumentModel>())).Returns(Task.CompletedTask);
            _service = serviceMock.Object;
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                await _service.UploadChunk(JsonConvert.SerializeObject(providerFormRequest), GenerateValidMockFile(), $"{VALID_FILE_NAME}", 0, VALID_DOCUMENT.Chunks.Count);
            });
            Assert.Null(exception);


            _documentRepository.Verify(repo => repo.FindByName(VALID_FILE_NAME), Times.Once);
            _documentRepository.Verify(repo => repo.SaveFile(VALID_FILE_NAME), Times.Never);
            _invoiceRepository.Verify(repo => repo.Create(VALID_INVOICE), Times.Never);
            _supaService.Verify(supa => supa.UploadFileChunk(It.IsAny<IFormFile>(), It.IsAny<DocumentChunkModel>()), Times.Once);
        }
        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
        private void MockService()
        {
            _supaService.Setup(supa => supa.DownloadChunk(It.IsAny<DocumentChunkModel>())).ReturnsAsync(VALID_RESPONSE.bytes);
            _supaService.Setup(supa => supa.UploadFileChunk(VALID_FILE, VALID_CHUNK)).ReturnsAsync(string.Empty);
            _invoiceService.Setup(s => s.Create(It.IsAny<Guid>(), It.IsAny<List<ExtractionMetadata>>())).Returns(Task.CompletedTask);
            //  _supaService.Setup(supa=>supa.)
        }
        private Task MockFactory()
        {
            var readerService = new Mock<PDFService>();
            readerService.Setup(rs => rs.ExtractDataFromDocument(It.IsAny<string>())).Returns(new List<ExtractionMetadata>());
            _readerFactory.Setup(f => f.GetReader(It.IsAny<string>())).Returns(readerService.Object);
            return Task.CompletedTask;
        }
        private Task MockRepository()
        {
            _documentRepository.Setup(repo => repo.FindByName(VALID_FILE_NAME)).ReturnsAsync(VALID_DOCUMENT);
            _documentRepository.Setup(repo => repo.FindByName(INVALID_FILE_NAME)).ReturnsAsync((DocumentModel?)null);

            _documentRepository.Setup(repo => repo.SaveFile(VALID_FILE_NAME)).ReturnsAsync(VALID_DOCUMENT);
            _documentRepository.Setup(repo => repo.SaveChunk(VALID_FILE, VALID_FILE_NAME, 0)).ReturnsAsync(VALID_CHUNK);
            _invoiceRepository.Setup(repo => repo.Create(VALID_INVOICE)).ReturnsAsync(VALID_INVOICE);

            _providerRepository.Setup(repo => repo.FindById(It.IsAny<Guid>())).ReturnsAsync(provider);
            _providerRepository.Setup(repo => repo.Create(It.IsAny<ProviderModel>())).ReturnsAsync(provider);
            return Task.CompletedTask;
        }
        public async Task InitializeAsync()
        {
            _documentRepository = new Mock<IDocumentRepository>();
            _supaService = new Mock<ICloudStorageService>();
            _invoiceRepository = new Mock<IInvoiceRepository>();
            _invoiceService = new Mock<IInvoiceService>();
            _readerFactory = new Mock<IDocumentReaderFactory>();
            _env = new Mock<IWebHostEnvironment>();
            _providerRepository = new Mock<IProviderRepository>();
            await MockRepository();
            MockService();
            await MockFactory();
            _env.Setup(env => env.WebRootPath)
           .Returns("C:\\FakeRoot");
            _service = new DocumentService(_invoiceService.Object, _documentRepository.Object, _supaService.Object, _invoiceRepository.Object, _env.Object, _readerFactory.Object, _providerRepository.Object);
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
