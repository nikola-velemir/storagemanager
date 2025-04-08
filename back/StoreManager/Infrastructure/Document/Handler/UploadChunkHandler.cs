using MediatR;
using Newtonsoft.Json;
using StoreManager.Infrastructure.Document.Command;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.Service.FileService;
using StoreManager.Infrastructure.Document.Service.Reader;
using StoreManager.Infrastructure.Document.SupaBase.Service;
using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.Invoice.Service;
using StoreManager.Infrastructure.Provider.DTO;
using StoreManager.Infrastructure.Provider.Model;
using StoreManager.Infrastructure.Provider.Repository;
using System.Text.RegularExpressions;

namespace StoreManager.Infrastructure.Document.Handler
{
    public class UploadChunkHandler : IRequestHandler<UploadChunkCommand>
    {
        private IProviderRepository _providerRepository;
        private IDocumentRepository _documentRepository;
        private IInvoiceRepository _invoiceRepository;
        private ICloudStorageService _supaService;
        private IFileService _fileService;
        private IDocumentReaderFactory _readerFactory;
        private IWebHostEnvironment _env;
        private IInvoiceService _invoiceService;

        public UploadChunkHandler(IProviderRepository providerRepository, IDocumentRepository documentRepository, IInvoiceRepository invoiceRepository, ICloudStorageService supaService, IFileService fileService, IDocumentReaderFactory readerFactory, IWebHostEnvironment env, IInvoiceService invoiceService)
        {
            _providerRepository = providerRepository;
            _documentRepository = documentRepository;
            _invoiceRepository = invoiceRepository;
            _supaService = supaService;
            _fileService = fileService;
            _readerFactory = readerFactory;
            _env = env;
            _invoiceService = invoiceService;
        }

        public async Task<Unit> Handle(UploadChunkCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var parsedProvider = JsonConvert.DeserializeObject<ProviderFormDataRequestDTO>(request.ProviderFormData);
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
                var parsedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(request.FileName), @"[^a-zA-Z0-9]", "");
                var foundFile = await _documentRepository.FindByName(parsedFileName);
                if (foundFile == null)
                {
                    foundFile = await _documentRepository.SaveFile(request.FileName);
                    var invoice = await _invoiceRepository.Create(new InvoiceModel
                    {
                        Provider = provider,
                        ProviderId = provider.Id,
                        DateIssued = DateOnly.FromDateTime(DateTime.UtcNow),
                        Document = foundFile,
                        DocumentId = foundFile.Id,
                        Id = Guid.NewGuid()
                    });
                    await _providerRepository.AddInvoice(provider, invoice);
                }
                var savedChunk = await _documentRepository.SaveChunk(request.File, request.FileName, request.ChunkIndex);
                await _supaService.UploadFileChunk(request.File, savedChunk);

                await _fileService.AppendChunk(request.File, foundFile);

                if (request.ChunkIndex == request.TotalChunks - 1)
                {
                    var documentReader = _readerFactory.GetReader(DocumentUtils.GetRawMimeType(foundFile.Type));

                    var webRootPath = Path.Combine(_env.WebRootPath, "uploads", "invoice");

                    var filePath = Path.Combine(webRootPath, $"{foundFile.Id.ToString()}.{DocumentUtils.GetRawMimeType(foundFile.Type)}");

                    var metadata = documentReader.ExtractDataFromDocument(filePath);

                    await _invoiceService.Create(foundFile.Id, metadata);

                    await _fileService.DeleteAllChunks(foundFile);
                }

                return Unit.Value;
            }
            catch (Exception)
            {
                throw new BadHttpRequestException("Failed to upload file!");
            }
        }
    }
}
