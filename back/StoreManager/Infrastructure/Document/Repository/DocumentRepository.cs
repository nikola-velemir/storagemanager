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
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DbSet<Domain.Document.Model.Document> _documents ;
        private readonly DbSet<DocumentChunk> _chunks ;

        public DocumentRepository(WarehouseDbContext context)
        {
            _documents = context.Documents;
            _chunks = context.DocumentChunks;
        }
        public Task<Domain.Document.Model.Document?> FindByDocumentId(Guid id)
        {
            return _documents.Include(doc => doc.Chunks).FirstOrDefaultAsync(doc => doc.Id.Equals(id));
        }

        public Task<Domain.Document.Model.Document?> FindByNameAsync(ISpecification<Domain.Document.Model.Document> spec, string fileName)
        {
            var query = spec.Apply(_documents);
            return query.FirstOrDefaultAsync(doc => doc.FileName == fileName);
        }
        public async Task<DocumentChunk> SaveChunkAsync(Domain.Document.Model.Document foundDoc, IFormFile? file, string fileName, int chunkIndex)
        {
            var processedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(fileName), @"[^a-zA-Z0-9]", "");
            if (file == null || file.Length == 0)
            {
                throw new NotFoundException("Invalid chunk");
            }
           
            var chunk = new DocumentChunk
            {
                ChunkNumber = chunkIndex,
                DocumentId = foundDoc.Id,
                Id = Guid.NewGuid(),
                SupaBasePath = Guid.NewGuid().ToString(),
                Document = foundDoc
            };
            var savedChunk = await _chunks.AddAsync(chunk);

            return savedChunk.Entity;
        }

        public Task<Domain.Document.Model.Document?> FindById(ISpecification<Domain.Document.Model.Document> spec, Guid id)
        {
            var query = spec.Apply(_documents.AsQueryable());
            return query.FirstOrDefaultAsync(doc => doc.Id.Equals(id));
        }

        public async Task<Domain.Document.Model.Document> SaveFileAsync(string fileName)
        {
            var parsedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(fileName), @"[^a-zA-Z0-9]", "");

            var mimeType = MimeMapping.MimeUtility.GetMimeMapping(fileName).Split('/').Last();

            var fileRecord = new Domain.Document.Model.Document
            {
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                FileName = parsedFileName,
                Type = mimeType,
                Id = Guid.NewGuid()
            };

            var savedInstance = await _documents.AddAsync(fileRecord);

            return savedInstance.Entity;
        }
    }
}
