
using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using System.Text.RegularExpressions;

namespace StoreManager.Infrastructure.Document.Repository
{
    public sealed class DocumentRepository : IDocumentRepository
    {
        private WarehouseDbContext _context;
        private DbSet<DocumentModel> _files;
        public DocumentRepository(WarehouseDbContext context)
        {
            _context = context;
            _files = context.Documents;
        }

        public Task<DocumentModel?> FindByName(string fileName)
        {
            return _files.FirstOrDefaultAsync(doc => doc.FileName == fileName);
        }

        public async Task<DocumentModel> SaveFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("No file found");
            }


            var fileGuid = Guid.NewGuid();
            var parsedFileName = Regex.Replace(file.FileName, @"[^a-zA-Z0-9]", "");
            var fileName = Path.GetFileNameWithoutExtension($"{fileGuid}_{Path.GetFileName(parsedFileName)}");
            var mimeType = MimeMapping.MimeUtility.GetMimeMapping(file.FileName).Split('/').Last();
            var fileRecord = new DocumentModel
            {
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                FileName = fileName,
                Type = mimeType,
                FileUrl = "",
                Id = Guid.NewGuid()
            };

            var savedInstance = await _files.AddAsync(fileRecord);
            await _context.SaveChangesAsync();

            return savedInstance.Entity;
        }
    }
}
