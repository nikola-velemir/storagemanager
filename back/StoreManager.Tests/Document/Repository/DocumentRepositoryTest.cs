using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using StoreManager.Infrastructure.DB;
using System.Text;
using StoreManager.Domain.Document.Model;
using StoreManager.Domain.Document.Specification;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.Document.Repository;

namespace StoreManager.Tests.Document.Repository
{
    public sealed class DocumentRepositoryTest : IAsyncLifetime
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private WarehouseDbContext _context;
        private DocumentRepository _repository;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private static readonly string VALID_FILE_NAME = "file";
        private static readonly string VALID_FILE_EXTENSION = "pdf";

        private static readonly string INVALID_FILE_NAME = "INVALID";
        private static readonly Guid VALID_FILE_ID = Guid.NewGuid();

        private static readonly Domain.Document.Model.Document VALID_DOCUMENT = new Domain.Document.Model.Document
        {
            Id = VALID_FILE_ID,
            Type = VALID_FILE_EXTENSION,
            Chunks = new List<DocumentChunk>(),
            Date = DateOnly.FromDateTime(DateTime.UtcNow),
            FileName = VALID_FILE_NAME
        };

        private static readonly DocumentChunk VALID_CHUNK = new DocumentChunk
            { Document = VALID_DOCUMENT, ChunkNumber = 0, DocumentId = VALID_FILE_ID };

        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        [Fact(DisplayName = "Find by name test - invalid name")]
        public async Task FindByName_InvalidNameTest()
        {
            var result = await _repository.FindById(new DocumentWithDocumentChunks(), INVALID_FILE_NAME);
            Assert.Null(result);
        }

        [Fact(DisplayName = "Find by name test - valid name")]
        public async Task FindByName_ValidNameTest()
        {
            var result = await _repository.FindById(new DocumentWithDocumentChunks(), VALID_FILE_NAME);
            Assert.NotNull(result);
            Assert.Equal(VALID_DOCUMENT, result);
        }

        [Fact(DisplayName = "Save file - valid name")]
        public async Task SaveFile_ValidNameTest()
        {
            var result = await _repository.SaveFileAsync($"{VALID_FILE_NAME}.{VALID_FILE_EXTENSION}");
            Assert.NotNull(result);
            Assert.Equal(VALID_FILE_NAME, result.FileName);
            Assert.Equal(VALID_FILE_EXTENSION, result.Type);
            Assert.Equal(DateOnly.FromDateTime(DateTime.UtcNow), result.Date);
        }

        [Fact(DisplayName = "Save chunk - null file test")]
        public async Task SaveChunk_NullFileTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var result = await _repository.SaveChunkAsync(null, $"{INVALID_FILE_NAME}.{VALID_FILE_EXTENSION}", 0);
            });
            Assert.NotNull(exception);
            Assert.Equal("Invalid chunk", exception.Message);
            Assert.IsType<FileNotFoundException>(exception);
        }

        [Fact(DisplayName = "Save chunk - empty file test")]
        public async Task SaveChunk_EmptyFileTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var result = await _repository.SaveChunkAsync(GenerateInvalidMockFile(),
                    $"{INVALID_FILE_NAME}.{VALID_FILE_EXTENSION}", 0);
            });
            Assert.NotNull(exception);
            Assert.Equal("Invalid chunk", exception.Message);
            Assert.IsType<FileNotFoundException>(exception);
        }

        [Fact(DisplayName = "Save chunk - invalid file name")]
        public async Task SaveChunk_InvalidFileNameTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var result = await _repository.SaveChunkAsync(GenerateValidMockFile(),
                    $"{INVALID_FILE_NAME}.{VALID_FILE_EXTENSION}", 0);
            });
            Assert.NotNull(exception);
            Assert.Equal("Invalid file", exception.Message);
            Assert.IsType<EntryPointNotFoundException>(exception);
        }

        [Fact(DisplayName = "Save chunk - valid test")]
        public async Task SaveChunk_ValidTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var result = await _repository.SaveChunkAsync(GenerateValidMockFile(),
                    $"{VALID_FILE_NAME}.{VALID_FILE_EXTENSION}", 0);
                Assert.Equal(VALID_DOCUMENT, result.Document);
                Assert.Equal(VALID_FILE_ID, result.DocumentId);
                Assert.Equal(0, result.ChunkNumber);
            });
            Assert.Null(exception);
        }

        public IFormFile GenerateValidMockFile()
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

        public IFormFile GenerateInvalidMockFile()
        {
            var fileName = $"{INVALID_FILE_NAME}.{VALID_FILE_EXTENSION}";
            var stream = new MemoryStream(); // Empty stream
            var formFile = new FormFile(stream, 0, 0, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/pdf"
            };
            return formFile;
        }

        private async Task SeedTestData()
        {
            _context.Documents.Add(VALID_DOCUMENT);
            await _context.SaveChangesAsync();
            _repository = new DocumentRepository(_context);
        }

        public async Task InitializeAsync()
        {
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new WarehouseDbContext(options);
            await SeedTestData();
        }
    }
}