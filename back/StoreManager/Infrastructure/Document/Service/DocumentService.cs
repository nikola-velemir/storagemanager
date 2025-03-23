using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.SupaBase.Service;
using System.Text.RegularExpressions;

namespace StoreManager.Infrastructure.Document.Service
{
    public sealed class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repository;
        private readonly SupabaseService _supaService;
        public DocumentService(IDocumentRepository repository)
        {
            _repository = repository;
            _supaService = new SupabaseService();
        }

        public async Task<DocumentDownloadResponseDTO> DownloadDocumentChunk(string fileName)
        {
            var file = await _repository.FindByName(fileName);

            if (file == null) throw new FileNotFoundException("Could not find the file");

            return new DocumentDownloadResponseDTO(new byte[0], "kurac");
        }
        public async Task<RequestDocumentDownloadResponseDTO> RequestDownload(string fileName) 
        {
            var file = await _repository.FindByName(fileName);
            if(file == null)
            {
                throw new FileNotFoundException("File not found");
            }
            return new RequestDocumentDownloadResponseDTO(file.FileName, GetMimeType(file.Type),file.Chunks.Count);
        }

        public async Task<DocumentDownloadResponseDTO> DownloadChunk(string fileName, int chunkIndex)
        {
            var file = await _repository.FindByName(fileName) 
                ?? throw new FileNotFoundException("File not found");

            var chunk = file.Chunks.FirstOrDefault(chunk => chunk.ChunkNumber == chunkIndex) 
                ?? throw new EntryPointNotFoundException("Chunk not found");

            return await _supaService.DownloadChunk(chunk);
            
        }

        public async Task DownloadFile(HttpResponse response, CancellationToken cancellationToken, string fileName)
        {
            var file = await _repository.FindByName(fileName);
            if (file == null) throw new FileNotFoundException("Could not find the file");

            var sortedChunks = file.Chunks.OrderBy(chunk => chunk.ChunkNumber).ToList();

            response.Headers.Append("Content-Type", GetMimeType(file.Type));
        //    response.Headers.Append("Transfer-Encoding", "chunked");
            response.Headers.Append("Cache-Control", "no-store");

            await using var responseStream = response.BodyWriter.AsStream();

            foreach (var chunk in sortedChunks)
            {
                var downloadedChunk = await _supaService.DownloadChunk(chunk);

                if (downloadedChunk == null || downloadedChunk.bytes.Length == 0)
                    continue;

                await responseStream.WriteAsync(downloadedChunk.bytes, cancellationToken);
                await responseStream.FlushAsync(cancellationToken); // 🔥 Ensures each chunk is sent immediately
            }
        }
        private string GetMimeType(string fileType) => fileType switch
        {
            "pdf" => "application/pdf",
            "jpg" => "image/jpeg",
            "png" => "image/png",
            "vnd.ms-excel" => "application/vnd.ms-excel",
            "vnd.openxmlformats-officedocument.spreadsheetml.sheet" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            _ => "application/octet-stream"
        };

        public async Task UploadChunk(IFormFile file, string fileName, int chunkIndex, int totalChunks)
        {
            try
            {
                var parsedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(fileName), @"[^a-zA-Z0-9]", "");
                var foundFile = await _repository.FindByName(parsedFileName);
                if (foundFile == null)
                {
                    foundFile = await _repository.SaveFile(fileName);
                }
                var savedChunk = await _repository.SaveChunk(file, fileName, chunkIndex);
                await _supaService.UploadFileChunk(file, savedChunk);
            }
            catch (Exception)
            {
                throw new BadHttpRequestException("Failed to upload file!");
            }
        }
        public async Task UploadFile(IFormFile file)
        {
            try
            {

                var savedFile = await _repository.SaveFile(file.FileName);
                await _supaService.UploadFile(file, savedFile);
            }
            catch (Exception)
            {
                throw new BadHttpRequestException("Failed to upload file!");
            }
        }
    }
}
