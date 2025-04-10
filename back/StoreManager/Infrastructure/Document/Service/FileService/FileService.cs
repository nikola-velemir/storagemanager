using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Document.Service.FileService
{
    public sealed class FileService(IWebHostEnvironment env) : IFileService
    {
        public async Task AppendChunk(IFormFile file, DocumentModel foundFile)
        {
            var webRootPath = Path.Combine(env.WebRootPath, "uploads", "invoice");
            if (!Directory.Exists(webRootPath))
            {
                Directory.CreateDirectory(webRootPath);

            }

            var filePath = Path.Combine(webRootPath, $"{foundFile.Id.ToString()}.{DocumentUtils.GetRawMimeType(foundFile.Type)}");

            if (!File.Exists(filePath))
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(fileStream);
                }
                return;
            }

            using (var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        public async Task DeleteAllChunks(DocumentModel file)
        {

            var webRootPath = Path.Combine(env.WebRootPath, "uploads", "invoice");
            var filePath = Path.Combine(webRootPath, $"{file.Id.ToString()}.{DocumentUtils.GetRawMimeType(file.Type)}");
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception)
                {
                    throw new IOException("Failed to delete file");
                }

            }
            await Task.CompletedTask;
        }
    }
}
