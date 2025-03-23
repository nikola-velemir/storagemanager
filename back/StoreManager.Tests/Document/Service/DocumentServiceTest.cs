using Microsoft.AspNetCore.Http;
using Moq;
using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.Service;
using StoreManager.Infrastructure.Document.SupaBase.Service;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.Tests.Document.Service
{
    public sealed class DocumentServiceTest : IAsyncLifetime
    {
        private Mock<IDocumentRepository> _repository;
        private Mock<ICloudStorageService> _supaService;
        private DocumentService _service;

        private static readonly string VALID_FILE_NAME = "file";

        private static readonly string INVALID_FILE_NAME = "INVALID";
        private static readonly string VALID_FILE_EXTENSION = "txt";
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

            _repository.Verify(repo => repo.FindByName(INVALID_FILE_NAME), Times.Once);
        }
        [Fact(DisplayName = "Request download - valid file name")]
        public async Task RequestDownload_ValidFileNameTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var result = await _service.RequestDownload(VALID_FILE_NAME);
                Assert.NotNull(result);
                Assert.Equal(VALID_DOWNLOAD_REQUEST_RESPONSE, result);
            });
            Assert.Null(exception);

            _repository.Verify(repo => repo.FindByName(VALID_FILE_NAME), Times.Once);
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

            _repository.Verify(repo => repo.FindByName(INVALID_FILE_NAME), Times.Once);
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

            _repository.Verify(repo => repo.FindByName(VALID_FILE_NAME), Times.Once);
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


            _repository.Verify(repo => repo.FindByName(VALID_FILE_NAME), Times.Once);
            _supaService.Verify(supa => supa.DownloadChunk(It.IsAny<DocumentChunkModel>()), Times.Once);
        }

        [Fact(DisplayName = "Upload chunk - valid test")]
        public async Task UploadChunk_ValidTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                await _service.UploadChunk(GenerateValidMockFile(), $"{VALID_FILE_NAME}", 0, VALID_DOCUMENT.Chunks.Count);

            });
            Assert.Null(exception);


            _repository.Verify(repo => repo.FindByName(VALID_FILE_NAME), Times.Once);
            _repository.Verify(repo => repo.SaveFile(VALID_FILE_NAME), Times.Never);
            _supaService.Verify(supa => supa.UploadFileChunk(It.IsAny<IFormFile>(), It.IsAny<DocumentChunkModel>()), Times.Once);
        }
        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
        private void MockSupabase()
        {
            _supaService.Setup(supa => supa.DownloadChunk(It.IsAny<DocumentChunkModel>())).ReturnsAsync(VALID_RESPONSE);
            _supaService.Setup(supa => supa.UploadFileChunk(VALID_FILE, VALID_CHUNK)).ReturnsAsync(string.Empty);
            //  _supaService.Setup(supa=>supa.)
        }
        private Task MockRepository()
        {
            _repository.Setup(repo => repo.FindByName(VALID_FILE_NAME)).ReturnsAsync(VALID_DOCUMENT);
            _repository.Setup(repo => repo.FindByName(INVALID_FILE_NAME)).ReturnsAsync((DocumentModel?)null);

            _repository.Setup(repo => repo.SaveFile(VALID_FILE_NAME)).ReturnsAsync(VALID_DOCUMENT);
            _repository.Setup(repo => repo.SaveChunk(VALID_FILE, VALID_FILE_NAME, 0)).ReturnsAsync(VALID_CHUNK);

            return Task.CompletedTask;
        }
        public async Task InitializeAsync()
        {
            _repository = new Mock<IDocumentRepository>();
            _supaService = new Mock<ICloudStorageService>();
            await MockRepository();
            MockSupabase();
            _service = new DocumentService(_repository.Object, _supaService.Object);
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
