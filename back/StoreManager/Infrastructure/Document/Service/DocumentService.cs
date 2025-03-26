using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.SupaBase.Service;
using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.Invoice.Repository;
using System.Text.RegularExpressions;

namespace StoreManager.Infrastructure.Document.Service
{
    public sealed class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ICloudStorageService _supaService;
        private readonly IInvoiceRepository _invoiceRepository;
        public DocumentService(IDocumentRepository repository, ICloudStorageService supabase, IInvoiceRepository invoiceRepository)
        {
            _documentRepository = repository;
            _supaService = supabase;
            _invoiceRepository = invoiceRepository;
        }
        public async Task<RequestDocumentDownloadResponseDTO> RequestDownload(string fileName)
        {
            var file = await _documentRepository.FindByName(fileName);
            if (file == null)
            {
                throw new FileNotFoundException("File not found");
            }
            return new RequestDocumentDownloadResponseDTO(file.FileName, GetMimeType(file.Type), file.Chunks.Count);
        }

        public async Task<DocumentDownloadResponseDTO> DownloadChunk(string fileName, int chunkIndex)
        {
            var file = await _documentRepository.FindByName(fileName)
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
            "txt" => "text/plain",
            "vnd.ms-excel" => "application/vnd.ms-excel",
            "vnd.openxmlformats-officedocument.spreadsheetml.sheet" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            _ => "application/octet-stream"
        };

        public async Task UploadChunk(IFormFile file, string fileName, int chunkIndex, int totalChunks)
        {
            try
            {
                var parsedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(fileName), @"[^a-zA-Z0-9]", "");
                var foundFile = await _documentRepository.FindByName(parsedFileName);
                if (foundFile == null)
                {
                    foundFile = await _documentRepository.SaveFile(fileName);
                    await _invoiceRepository.Save(new InvoiceModel
                    {
                        DateIssued = DateOnly.FromDateTime(DateTime.UtcNow),
                        Document = foundFile,
                        DocumentId = foundFile.Id,
                        Id = Guid.NewGuid()
                    });

                }
                var savedChunk = await _documentRepository.SaveChunk(file, fileName, chunkIndex);
                await _supaService.UploadFileChunk(file, savedChunk);
            }
            catch (Exception)
            {
                throw new BadHttpRequestException("Failed to upload file!");
            }
        }
    }
}
