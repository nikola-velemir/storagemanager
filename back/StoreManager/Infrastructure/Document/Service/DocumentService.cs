using Newtonsoft.Json;
using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.Service.FileService;
using StoreManager.Infrastructure.Document.Service.Reader;
using StoreManager.Infrastructure.Document.SupaBase.Service;
using StoreManager.Infrastructure.MiddleWare.Exceptions;
using System.Text.RegularExpressions;
using StoreManager.Infrastructure.BusinessPartner.Base;
using StoreManager.Infrastructure.BusinessPartner.Base.Model;
using StoreManager.Infrastructure.BusinessPartner.Provider.DTO;
using StoreManager.Infrastructure.BusinessPartner.Provider.Model;
using StoreManager.Infrastructure.BusinessPartner.Provider.Repository;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Invoice.Import.Repository;
using StoreManager.Infrastructure.Invoice.Import.Service;

namespace StoreManager.Infrastructure.Document.Service
{
    public class DocumentService(
        IImportService importService,
        IDocumentRepository repository,
        ICloudStorageService supabase,
        IImportRepository importRepository,
        IWebHostEnvironment env,
        IDocumentReaderFactory readerFactory,
        IProviderRepository providerRepository,
        IFileService fileService)
        : IDocumentService
    {
        public async Task<DocumentDownloadResponseDto> DownloadChunk(string invoiceId, int chunkIndex)
        {
            if (!Guid.TryParse(invoiceId, out var tempId))
            {
                throw new InvalidCastException("Guid cannot be parsed");
            }

            var invoiceGuid = Guid.Parse(invoiceId);
            var invoice = await importRepository.FindById(invoiceGuid);
            if (invoice is null)
            {
                throw new NotFoundException("Invoice not found");
            }

            var file = await repository.FindByName(invoice.Document.FileName) ??
                       throw new NotFoundException("File not found");


            var chunk = file.Chunks.FirstOrDefault(chunk => chunk.ChunkNumber == chunkIndex)
                        ?? throw new NotFoundException("Chunk not found");

            var response = new DocumentDownloadResponseDto(await supabase.DownloadChunk(chunk),
                DocumentUtils.GetPresentationalMimeType(file.Type));
            return response;
        }

        public async Task UploadChunk(string providerFormData, IFormFile file, string fileName, int chunkIndex,
            int totalChunks)
        {
            try
            {
                var parsedProvider = JsonConvert.DeserializeObject<ProviderFormDataRequestDto>(providerFormData);
                if (parsedProvider is null)
                {
                    throw new ArgumentNullException("provider is null");
                }

                ProviderModel? provider;
                if (!string.IsNullOrEmpty(parsedProvider.ProviderId))
                {
                    provider = await providerRepository.FindById(Guid.Parse(parsedProvider.ProviderId));
                    if (provider is null) throw new ArgumentNullException("provider is null");
                }
                else
                {
                    provider = await providerRepository.Create(new ProviderModel
                    {
                        Address = parsedProvider.ProviderAddress,
                        Id = Guid.NewGuid(), 
                        Type = BusinessPartnerType.Provider,
                        Name = parsedProvider.ProviderName,
                        PhoneNumber = parsedProvider.ProviderPhoneNumber
                    });
                }

                var parsedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(fileName), @"[^a-zA-Z0-9]", "");
                var foundFile = await repository.FindByName(parsedFileName);
                if (foundFile == null)
                {
                    foundFile = await repository.SaveFile(fileName);
                    var invoice = await importRepository.Create(new ImportModel
                    {
                        Provider = provider,
                        ProviderId = provider.Id,
                        DateIssued = DateOnly.FromDateTime(DateTime.UtcNow),
                        Document = foundFile,
                        DocumentId = foundFile.Id,
                        Id = Guid.NewGuid()
                    });
                    await providerRepository.AddInvoice(provider, invoice);
                }

                var savedChunk = await repository.SaveChunk(file, fileName, chunkIndex);
                await supabase.UploadFileChunk(file, savedChunk);

                await fileService.AppendChunk(file, foundFile);

                if (chunkIndex == totalChunks - 1)
                {
                    var documentReader = readerFactory.GetReader(DocumentUtils.GetRawMimeType(foundFile.Type));

                    var webRootPath = Path.Combine(env.WebRootPath, "uploads", "invoice");

                    var filePath = Path.Combine(webRootPath,
                        $"{foundFile.Id.ToString()}.{DocumentUtils.GetRawMimeType(foundFile.Type)}");

                    var metadata = documentReader.ExtractDataFromDocument(filePath);

                    await importService.Create(foundFile.Id, metadata);

                    await fileService.DeleteAllChunks(foundFile);
                }
            }
            catch (Exception)
            {
                throw new BadHttpRequestException("Failed to upload file!");
            }
        }
    }
}