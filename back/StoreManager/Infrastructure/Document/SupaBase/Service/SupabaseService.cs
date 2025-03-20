using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Document.DTO;
using Supabase;
using Supabase.Storage;
using Supabase.Storage.Interfaces;
using System.Net.Mime;
using System.Net.Sockets;

namespace StoreManager.Infrastructure.Document.SupaBase.Service
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

        public async Task<string> UploadFile(IFormFile file, DocumentModel document)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("No file found");
            }

            var storage = _client.Storage.From(bucketName);
            var fileGuid = document.Id;
            var mimeType = MimeMapping.MimeUtility.GetMimeMapping(file.FileName).Split('/').Last();
            var pathName = Path.Combine("invoice", document.Date.ToString("yyyy-MM-dd"), $"{fileGuid.ToString()}.{mimeType}");
            var response = string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();

                response = await storage.Upload(fileBytes, pathName, new Supabase.Storage.FileOptions { ContentType = mimeType });

            }
            return response;
        }
        public async Task<DocumentDownloadResponseDTO> DownloadFile(DocumentModel document)
        {
            var storage = _client.Storage.From(bucketName);
            var fileGuid = document.Id;
            var mimeType = document.Type;
            var documentName = $"{fileGuid.ToString()}.{document.Type}";
            var pathName = Path.Combine("invoice", document.Date.ToString("yyyy-MM-dd"), documentName);

            var file = await storage.Download(pathName, new TransformOptions { });
            string transformedType = mimeType switch
            {
                "pdf" => "application/pdf",
                "jpg" => "image/jpeg",
                "png" => "image/png",
                "vnd.ms-excel" => "application/vnd.ms-excel",
                "vnd.openxmlformats-officedocument.spreadsheetml.sheet" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream"
            };
            return new DocumentDownloadResponseDTO(file, transformedType);
        }
    }
}