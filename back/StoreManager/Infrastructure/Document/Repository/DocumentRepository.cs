
using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Document.Model;
using System.Text.RegularExpressions;

namespace StoreManager.Infrastructure.Document.Repository
{
    public sealed class DocumentRepository : IDocumentRepository
    {
        private WarehouseDbContext _context;
        private DbSet<DocumentModel> _files;
        private DbSet<DocumentChunkModel> _chunks;
        private IWebHostEnvironment _env; private string _uploadPath;
        public DocumentRepository(WarehouseDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _files = context.Documents;
            _chunks = context.DocumentChunks;
            _env = env;
            _uploadPath = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        public bool AreAllChunksReceived(string fileName, int totalChunks)
        {
            var files = Directory.GetFiles(_uploadPath, $"{fileName}.part*");
            return files.Length == totalChunks;
        }

        public Task<DocumentModel?> FindByName(string fileName)
        {
            return _files.Include(doc => doc.Chunks).FirstOrDefaultAsync(doc => doc.FileName == fileName);
        }

        public async Task<FileInfo> MergeChunks(string fileName, int totalChunks)
        {
            var finalPath = Path.Combine(_uploadPath, fileName);

            using (var stream = new FileStream(finalPath, FileMode.Create))
            {
                for (int i = 0; i < totalChunks; ++i)
                {
                    var chunkPath = Path.Combine(_uploadPath, $"{fileName}.part{i}");
                    using (var chunkStream = new FileStream(chunkPath, FileMode.Open))
                    {
                        await chunkStream.CopyToAsync(stream);
                    }
                    System.IO.File.Delete(chunkPath);
                }
            }
            return new FileInfo(finalPath);
        }

        public async Task<DocumentChunkModel> SaveChunk(IFormFile file, string fileName, int chunkIndex)
        {
            var processedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(fileName), @"[^a-zA-Z0-9]", "");
            if (file == null || file.Length == 0)
            {
                throw new Exception("Invalid chunk");
            }
            var foundDoc = await FindByName(processedFileName);
            if (foundDoc == null)
            {
                throw new Exception("Invalid file");
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
        //public async Task<DocumentModel> SaveFile(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        throw new Exception("No file found");
        //    }


        //    var fileGuid = Guid.NewGuid();
        //    var parsedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(file.FileName), @"[^a-zA-Z0-9]", "");

        //    //  var fileName = Path.GetFileNameWithoutExtension($"{fileGuid}_{Path.GetFileName(parsedFileName)}");
        //    var fileName = parsedFileName;
        //    var mimeType = MimeMapping.MimeUtility.GetMimeMapping(file.FileName).Split('/').Last();
        //    var filePath = Path.Combine(_uploadPath, fileName);
        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    var fileRecord = new DocumentModel
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.UtcNow),
        //        FileName = fileName,
        //        Type = mimeType,
        //        Id = Guid.NewGuid()
        //    };

        //    var savedInstance = await _files.AddAsync(fileRecord);
        //    await _context.SaveChangesAsync();

        //    return savedInstance.Entity;
        //}
        public async Task<DocumentModel> SaveFile(string fileName)
        {
            var fileGuid = Guid.NewGuid();
            var parsedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(fileName), @"[^a-zA-Z0-9]", "");

            //  var fileName = Path.GetFileNameWithoutExtension($"{fileGuid}_{Path.GetFileName(parsedFileName)}");
            var mimeType = MimeMapping.MimeUtility.GetMimeMapping(fileName).Split('/').Last();
            var filePath = Path.Combine(_uploadPath, parsedFileName);


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
