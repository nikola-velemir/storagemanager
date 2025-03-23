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
        private readonly ICloudStorageService _supaService;
        public DocumentService(IDocumentRepository repository, ICloudStorageService supabase)
        {
            _repository = repository;
            _supaService = supabase;
        }
        public async Task<RequestDocumentDownloadResponseDTO> RequestDownload(string fileName)
        {
            var file = await _repository.FindByName(fileName);
            if (file == null)
            {
                throw new FileNotFoundException("File not found");
            }
            return new RequestDocumentDownloadResponseDTO(file.FileName, GetMimeType(file.Type), file.Chunks.Count);
        }

        public async Task<DocumentDownloadResponseDTO> DownloadChunk(string fileName, int chunkIndex)
        {
            var file = await _repository.FindByName(fileName)
                ?? throw new FileNotFoundException("File not found");

            var chunk = file.Chunks.FirstOrDefault(chunk => chunk.ChunkNumber == chunkIndex)
                ?? throw new EntryPointNotFoundException("Chunk not found");

            return await _supaService.DownloadChunk(chunk);

        }

        private string GetMimeType(string fileType) => fileType switch
        {
            "pdf" => "application/pdf",
            "jpg" => "image/jpeg",
            "png" => "image/png",
            "txt"=>"text/plain",
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
    }
}
