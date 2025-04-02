using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.Service.Reader;
using StoreManager.Infrastructure.Document.SupaBase.Service;
using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.Invoice.Service;
using System.Text.RegularExpressions;

namespace StoreManager.Infrastructure.Document.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ICloudStorageService _supaService;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceService _invoiceService;
        private readonly IWebHostEnvironment _env;
        private readonly IDocumentReaderFactory _readerFactory;
        public DocumentService(IInvoiceService invoiceService, IDocumentRepository repository, ICloudStorageService supabase, IInvoiceRepository invoiceRepository, IWebHostEnvironment env, IDocumentReaderFactory readerFactory)
        {
            _invoiceService = invoiceService;
            _documentRepository = repository;
            _supaService = supabase;
            _invoiceRepository = invoiceRepository;
            _env = env;
            _readerFactory = readerFactory;
        }
        public async Task<RequestDocumentDownloadResponseDTO> RequestDownload(string fileName)
        {
            var file = await _documentRepository.FindByName(fileName);
            if (file == null)
            {
                throw new FileNotFoundException("File not found");
            }
            return new RequestDocumentDownloadResponseDTO(file.FileName, GetPresentationalMimeType(file.Type), file.Chunks.Count);
        }
        public async Task<DocumentDownloadResponseDTO> DownloadChunk(string fileName, int chunkIndex)
        {
            var file = await _documentRepository.FindByName(fileName)
                ?? throw new FileNotFoundException("File not found");

            var chunk = file.Chunks.FirstOrDefault(chunk => chunk.ChunkNumber == chunkIndex)
                ?? throw new EntryPointNotFoundException("Chunk not found");

            var response = new DocumentDownloadResponseDTO(await _supaService.DownloadChunk(chunk), GetPresentationalMimeType(file.Type));
            return response;

        }
        private string GetRawMimeType(string fileType) => fileType switch
        {
            "pdf" => "pdf",
            "jpg" => "image/jpeg",
            "png" => "image/png",
            "txt" => "text/plain",
            "vnd.ms-excel" => "xlsx",
            "vnd.openxmlformats-officedocument.spreadsheetml.sheet" => "xlsx",
            _ => "application/octet-stream"
        };
        private string GetPresentationalMimeType(string fileType) => fileType switch
        {
            "pdf" => "application/pdf",
            "jpg" => "image/jpeg",
            "png" => "image/png",
            "txt" => "text/plain",
            "text/plain" => "text/plain",
            "vnd.ms-excel" => "application/vnd.ms-excel",
            "vnd.openxmlformats-officedocument.spreadsheetml.sheet" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            _ => "application/octet-stream"
        };
        public virtual async Task AppendChunk(IFormFile file, DocumentModel foundFile)
        {
            var webRootPath = Path.Combine(_env.WebRootPath, "uploads", "invoice");
            if (!Directory.Exists(webRootPath))
            {
                Directory.CreateDirectory(webRootPath);

            }

            var filePath = Path.Combine(webRootPath, $"{foundFile.Id.ToString()}.{GetRawMimeType(foundFile.Type)}");  // Set the correct file extension

            // For new files
            if (!File.Exists(filePath))
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(fileStream);
                }
                return;
            }

            // For appending to existing files
            using (var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        public async Task UploadChunk(IFormFile file, string fileName, int chunkIndex, int totalChunks)
        {
            try
            {
                var parsedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(fileName), @"[^a-zA-Z0-9]", "");
                var foundFile = await _documentRepository.FindByName(parsedFileName);
                if (foundFile == null)
                {
                    foundFile = await _documentRepository.SaveFile(fileName);
                    await _invoiceRepository.Create(new InvoiceModel
                    {
                        DateIssued = DateOnly.FromDateTime(DateTime.UtcNow),
                        Document = foundFile,
                        DocumentId = foundFile.Id,
                        Id = Guid.NewGuid()
                    });

                }
                var savedChunk = await _documentRepository.SaveChunk(file, fileName, chunkIndex);
                await _supaService.UploadFileChunk(file, savedChunk);

                await AppendChunk(file, foundFile);

                var documentReader = _readerFactory.GetReader(GetRawMimeType(foundFile.Type));

                var webRootPath = Path.Combine(_env.WebRootPath, "uploads", "invoice");

                var filePath = Path.Combine(webRootPath, $"{foundFile.Id.ToString()}.{GetRawMimeType(foundFile.Type)}");

                var metadata = documentReader.ExtractDataFromDocument(filePath);

                await _invoiceService.Create(foundFile.Id, metadata);

            }
            catch (Exception)
            {
                throw new BadHttpRequestException("Failed to upload file!");
            }
        }
    }
}
