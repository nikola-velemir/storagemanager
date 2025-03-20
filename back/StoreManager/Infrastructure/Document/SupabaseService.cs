using StoreManager.Infrastructure.DB;
using Supabase;
using Supabase.Storage;
using Supabase.Storage.Interfaces;
using System.Net.Mime;

namespace StoreManager.Infrastructure.Document
{
    public sealed class SupabaseService
    {
        private readonly Supabase.Client _client;
        private readonly string bucketName = "store-manager-docs";
        public SupabaseService()
        {
            var url = Environment.GetEnvironmentVariable("SUPABASE_URL", EnvironmentVariableTarget.User) ??
                throw new ArgumentException();
            var key = Environment.GetEnvironmentVariable("SUPABASE_API_KEY", EnvironmentVariableTarget.User) ??
                throw new ArgumentException(); ;
            _client = new Supabase.Client(url, key, new SupabaseOptions { AutoConnectRealtime = true });
        }

        public async Task<string> UploadFile(IFormFile file, DocumentModel fileModel)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("No file found");
            }

            var storage = _client.Storage.From(bucketName);
            var fileGuid = fileModel.FileName;
            var mimeType = MimeMapping.MimeUtility.GetMimeMapping(file.FileName).Split('/').Last();
            var pathName = Path.Combine("invoice",fileModel.Date.ToString("yyyy-MM-dd"),$"{ fileGuid.ToString()}.{ mimeType}");
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();

                var response = await storage.Upload(fileBytes, pathName, new Supabase.Storage.FileOptions { ContentType = mimeType });

            }

            return "";

        }
    }
}
