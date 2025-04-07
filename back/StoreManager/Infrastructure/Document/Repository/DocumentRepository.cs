
using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;

using StoreManager.Infrastructure.Document.Model;
using System.Text.RegularExpressions;
namespace StoreManager.Infrastructure.Document.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private WarehouseDbContext _context;
        private DbSet<DocumentModel> _files;
        private DbSet<DocumentChunkModel> _chunks;
        public DocumentRepository(WarehouseDbContext context)
        {
            _context = context;
            _files = context.Documents;
            _chunks = context.DocumentChunks;
        }

        public Task<DocumentModel?> FindByDocumentId(Guid id)
        {
            return _files.Include(doc => doc.Chunks).FirstOrDefaultAsync(doc => doc.Id.Equals(id));
        }

        public Task<DocumentModel?> FindByName(string fileName)
        {
            return _files.Include(doc => doc.Chunks).FirstOrDefaultAsync(doc => doc.FileName == fileName);
        }
        public async Task<DocumentChunkModel> SaveChunk(IFormFile? file, string fileName, int chunkIndex)
        {
            var processedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(fileName), @"[^a-zA-Z0-9]", "");
            if (file == null || file.Length == 0)
            {
                throw new FileNotFoundException("Invalid chunk");
            }
            var foundDoc = await FindByName(processedFileName);
            if (foundDoc == null)
            {
                throw new EntryPointNotFoundException("Invalid file");
            }
            var chunk = new DocumentChunkModel
            {
                ChunkNumber = chunkIndex,
                DocumentId = foundDoc.Id,
                Id = Guid.NewGuid(),
                SupaBasePath = Guid.NewGuid().ToString(),
                Document = foundDoc
            };
            var savedChunk = await _chunks.AddAsync(chunk);

            await _context.SaveChangesAsync();
            return savedChunk.Entity;
        }

        public async Task<DocumentModel> SaveFile(string fileName)
        {
            var fileGuid = Guid.NewGuid();
            var parsedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(fileName), @"[^a-zA-Z0-9]", "");

            var mimeType = MimeMapping.MimeUtility.GetMimeMapping(fileName).Split('/').Last();

            var fileRecord = new DocumentModel
            {
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                FileName = parsedFileName,
                Type = mimeType,
                Id = Guid.NewGuid()
            };

            var savedInstance = await _files.AddAsync(fileRecord);
            await _context.SaveChangesAsync();

            return savedInstance.Entity;
        }
    }
}
