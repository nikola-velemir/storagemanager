using Newtonsoft.Json;
using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.Service.Reader;
using StoreManager.Infrastructure.Document.SupaBase.Service;
using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.Invoice.Service;
using StoreManager.Infrastructure.Provider.DTO;
using StoreManager.Infrastructure.Provider.Model;
using StoreManager.Infrastructure.Provider.Repository;
using System.Net;
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
        private readonly IProviderRepository _providerRepository;
        private readonly IDocumentReaderFactory _readerFactory;
        private static readonly ProviderModel provider = new ProviderModel { Adress = "aaa", Id = Guid.NewGuid(), Name = "kita", PhoneNumber = "adsa" };
        public DocumentService(IInvoiceService invoiceService, IDocumentRepository repository, ICloudStorageService supabase, IInvoiceRepository invoiceRepository, IWebHostEnvironment env, IDocumentReaderFactory readerFactory, IProviderRepository providerRepository)
        {
            _invoiceService = invoiceService;
            _documentRepository = repository;
            _supaService = supabase;
            _invoiceRepository = invoiceRepository;
            _env = env;
            _readerFactory = readerFactory;
            _providerRepository = providerRepository;
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

        public virtual async Task DeleteAllChunks(DocumentModel file)
        {

            var webRootPath = Path.Combine(_env.WebRootPath, "uploads", "invoice");
            var filePath = Path.Combine(webRootPath, $"{file.Id.ToString()}.{GetRawMimeType(file.Type)}");
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
        public virtual async Task AppendChunk(IFormFile file, DocumentModel foundFile)
        {
            var webRootPath = Path.Combine(_env.WebRootPath, "uploads", "invoice");
            if (!Directory.Exists(webRootPath))
            {
                Directory.CreateDirectory(webRootPath);

            }

            var filePath = Path.Combine(webRootPath, $"{foundFile.Id.ToString()}.{GetRawMimeType(foundFile.Type)}");

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
        public async Task UploadChunk(string providerFormData, IFormFile file, string fileName, int chunkIndex, int totalChunks)
        {
            try
            {
                var parsedProvider = JsonConvert.DeserializeObject<ProviderFormDataRequestDTO>(providerFormData);
                if (parsedProvider is null)
                {
                    throw new ArgumentNullException("provider is null");
                }
                ProviderModel? provider;
                if (!string.IsNullOrEmpty(parsedProvider.providerId))
                {
                    provider = await _providerRepository.FindById(Guid.Parse(parsedProvider.providerId));
                    if (provider is null) throw new ArgumentNullException("provider is null");

                }
                else
                {
                    provider = await _providerRepository.Create(new ProviderModel
                    {
                        Adress = parsedProvider.providerAddress,
                        Id = Guid.NewGuid(),
                        Name = parsedProvider.providerName,
                        PhoneNumber = parsedProvider.providerPhoneNumber
                    });
                }
                var parsedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(fileName), @"[^a-zA-Z0-9]", "");
                var foundFile = await _documentRepository.FindByName(parsedFileName);
                if (foundFile == null)
                {
                    foundFile = await _documentRepository.SaveFile(fileName);
                    await _invoiceRepository.Create(new InvoiceModel
                    {
                        Provider = provider,
                        ProviderId = provider.Id,
                        DateIssued = DateOnly.FromDateTime(DateTime.UtcNow),
                        Document = foundFile,
                        DocumentId = foundFile.Id,
                        Id = Guid.NewGuid()
                    });


                }
                var savedChunk = await _documentRepository.SaveChunk(file, fileName, chunkIndex);
                await _supaService.UploadFileChunk(file, savedChunk);

                await AppendChunk(file, foundFile);

                if (chunkIndex == totalChunks - 1)
                {
                    var documentReader = _readerFactory.GetReader(GetRawMimeType(foundFile.Type));

                    var webRootPath = Path.Combine(_env.WebRootPath, "uploads", "invoice");

                    var filePath = Path.Combine(webRootPath, $"{foundFile.Id.ToString()}.{GetRawMimeType(foundFile.Type)}");

                    var metadata = documentReader.ExtractDataFromDocument(filePath);

                    await _invoiceService.Create(foundFile.Id, metadata);

                    await DeleteAllChunks(foundFile);
                }


            }
            catch (Exception)
            {
                throw new BadHttpRequestException("Failed to upload file!");
            }
        }
    }
}
