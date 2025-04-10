using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Model;
using Supabase;
using Supabase.Storage;

namespace StoreManager.Infrastructure.Document.SupaBase.Service
{
    public sealed class SupabaseService : ICloudStorageService
    {
        private readonly Supabase.Client _client;
        private readonly string _bucketName = "store-manager-docs";
        public SupabaseService()
        {
            var url = Environment.GetEnvironmentVariable("SUPABASE_URL", EnvironmentVariableTarget.User) ??
                throw new ArgumentException();
            var key = Environment.GetEnvironmentVariable("SUPABASE_API_KEY", EnvironmentVariableTarget.User) ??
                throw new ArgumentException(); ;
            _client = new Supabase.Client(url, key, new SupabaseOptions { AutoConnectRealtime = true });
        }
        public async Task<string> UploadFileChunk(IFormFile fileChunk, DocumentChunkModel chunk)
        {
            if (fileChunk == null || fileChunk.Length == 0)
            {
                throw new Exception("No file found");
            }
            var storage = _client.Storage.From(_bucketName);
            var fileGuid = chunk.Id;
            var pathName = Path.Combine("invoice", chunk.Document.Date.ToString("yyyy-MM-dd"),$"{chunk.Document.Id}", $"{fileGuid.ToString()}");
            var response = string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                await fileChunk.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();

                response = await storage.Upload(fileBytes, pathName);

            }
            return response;

        }
       
        public async Task<byte[]> DownloadChunk(DocumentChunkModel chunk)
        {
            var storage = _client.Storage.From(_bucketName);
            var fileGuid = chunk.Id;
            var mimeType = chunk.Document.Type;
            var pathName = Path.Combine("invoice", chunk.Document.Date.ToString("yyyy-MM-dd"), chunk.Document.Id.ToString(),chunk.Id.ToString());

            var file = await storage.Download(pathName, new TransformOptions { });
            
            return file;
        }
    }
}