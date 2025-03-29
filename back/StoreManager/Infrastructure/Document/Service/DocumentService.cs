using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.SupaBase.Service;
using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.Invoice.Repository;
using System.Text.RegularExpressions;

namespace StoreManager.Infrastructure.Document.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ICloudStorageService _supaService;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IWebHostEnvironment _env;
        public DocumentService(IDocumentRepository repository, ICloudStorageService supabase, IInvoiceRepository invoiceRepository, IWebHostEnvironment env)
        {
            _documentRepository = repository;
            _supaService = supabase;
            _invoiceRepository = invoiceRepository;
            _env = env;
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
        public virtual async Task LoadAndSaveFile(string fileName)
        {
            var file = await _documentRepository.FindByName(fileName);
            if (file == null)
            {
                throw new FileNotFoundException("Not found!");
            }
            var sortedChunks = file.Chunks.OrderBy(r => r.ChunkNumber).ToList();
            var allChunks = new List<byte[]>();
            foreach (var chunk in sortedChunks)
            {
                var response = await DownloadChunk(fileName, chunk.ChunkNumber);
                allChunks.Add(response.bytes);
            }
            var finalBytes = allChunks.SelectMany(b => b).ToArray();
            var webRootPath = Path.Combine(_env.WebRootPath, "uploads", "invoice", $"{file.Id.ToString()}");
            if (!Directory.Exists(webRootPath))
            {
                Directory.CreateDirectory(webRootPath);

            }
            var finalFilePath = Path.Combine(webRootPath, $"{file.Id.ToString()}.{GetRawMimeType(file.Type)}");
            await File.WriteAllBytesAsync(finalFilePath, finalBytes);

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

                // var webRootPath = Path.Combine(_env.WebRootPath, "uploads", "invoice", $"{foundFile.Id.ToString()}");
                // if (!Directory.Exists(webRootPath))
                // {
                //     Directory.CreateDirectory(webRootPath);
                //     using (var fileStream = new FileStream(webRootPath, FileMode.Create, FileAccess.Write))
                //     {
                //         await file.CopyToAsync(fileStream);
                //     }
                //     return ;
                // }
                // using(var fileSteam = new FileStream(webRootPath, FileMode.Append, FileAccess.Write))
                // {
                //     await file.CopyToAsync(fileSteam);
                // }
            }
            catch (Exception)
            {
                throw new BadHttpRequestException("Failed to upload file!");
            }
        }
    }
}
