using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using StoreManager.Application.Document.Repository;
using StoreManager.Domain.Document.Model;
using StoreManager.Domain.Document.Specification;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.MiddleWare.Exceptions;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Document.Repository
{
    public class DocumentRepository(WarehouseDbContext context) : IDocumentRepository
    {
        private readonly DbSet<DocumentModel> _files = context.Documents;
        private readonly DbSet<DocumentChunkModel> _chunks = context.DocumentChunks;

        public Task<DocumentModel?> FindByDocumentId(Guid id)
        {
            return _files.Include(doc => doc.Chunks).FirstOrDefaultAsync(doc => doc.Id.Equals(id));
        }

        public Task<DocumentModel?> FindByNameAsync(ISpecification<DocumentModel> spec, string fileName)
        {
            var query = spec.Apply(_files);
            return query.FirstOrDefaultAsync(doc => doc.FileName == fileName);
        }

        public async Task<DocumentChunkModel> SaveChunkAsync(IFormFile? file, string fileName, int chunkIndex)
        {
            var processedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(fileName), @"[^a-zA-Z0-9]", "");
            if (file == null || file.Length == 0)
            {
                throw new NotFoundException("Invalid chunk");
            }
            var foundDoc = await FindByNameAsync(new DocumentWithDocumentChunks(), processedFileName);
            if (foundDoc == null)
            {
                throw new NotFoundException("Invalid file");
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

            await context.SaveChangesAsync();
            return savedChunk.Entity;
        }

        public async Task<DocumentModel> SaveFileAsync(string fileName)
        {
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
            await context.SaveChangesAsync();

            return savedInstance.Entity;
        }
    }
}
