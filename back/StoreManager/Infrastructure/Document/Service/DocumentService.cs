using Newtonsoft.Json;
using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.Service.FileService;
using StoreManager.Infrastructure.Document.Service.Reader;
using StoreManager.Infrastructure.Document.SupaBase.Service;
using StoreManager.Infrastructure.MiddleWare.Exceptions;
using System.Text.RegularExpressions;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
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
                {
                    if (parsedProvider is null)
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

        public async Task<byte[]> GeneratePdfFile(List<ProductRow> rows, string fileName)
        {
            using var stream = new MemoryStream();
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var headerFont = new XFont("Helvetica#", 14, XFontStyleEx.Bold);
            var textFont = new XFont("Helvetica#", 12, XFontStyleEx.Regular);


            const double margin = 40;
            const double tableTop = 80;
            const double rowHeight = 25;
            string[] headers = { "Name", "Identifier", "Quantity", "Price", };
            double[] colWidths = { 60, 200, 80, 60 };

            double x = margin;
            double y = tableTop;

            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(XPens.Black, XBrushes.LightGray, x, y, colWidths[i], rowHeight);
                gfx.DrawString(headers[i], textFont, XBrushes.Black,
                    new XRect(x, y, colWidths[i], rowHeight), XStringFormats.Center);
                x += colWidths[i];
            }

            y += rowHeight;

            foreach (var row in rows)
            {
                x = margin;

                string[] values =
                {
                    row.Name,
                    row.Identifier,
                    row.Quantity.ToString(),
                    row.Price.ToString("C"),
                };

                for (int i = 0; i < values.Length; i++)
                {
                    gfx.DrawRectangle(XPens.Black, x, y, colWidths[i], rowHeight);
                    gfx.DrawString(values[i], textFont, XBrushes.Black,
                        new XRect(x + 2, y + 2, colWidths[i] - 4, rowHeight - 4), XStringFormats.TopLeft);
                    x += colWidths[i];
                }

                y += rowHeight;
            }

            // Save PDF to memory stream
            document.Save(stream, false);
            stream.Position = 0;

            return stream.ToArray();
        }

        private static List<IFormFile> ConvertToFormFiles(List<byte[]> chunks, string originalFileName)
        {
            var formFiles = new List<IFormFile>();

            for (int i = 0; i < chunks.Count; i++)
            {
                var chunk = chunks[i];
                var stream = new MemoryStream(chunk); // wrap the byte[] in a stream

                var formFile = new FormFile(stream, 0, chunk.Length, $"chunk_{i}",
                    $"{Path.GetFileNameWithoutExtension(originalFileName)}_part{i + 1}.pdf")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/pdf"
                };

                formFiles.Add(formFile);
            }

            return formFiles;
        }

        private static async Task<List<byte[]>> ConvertToChunks(byte[] file)
        {
            const double chunkSize = 0.5 * 1024 * 1024;
            
            var chunks = new List<byte[]>();
            var offset = 0;

            while (offset < file.Length)
            {
                var size = (int)Math.Min(chunkSize, file.Length - offset);
                var chunk = new byte[size];
                Buffer.BlockCopy(file, offset, chunk, 0, size);
                chunks.Add(chunk);
                offset += size;
            }

            return chunks;
        }

        public async Task<DocumentModel> UploadExport(List<ProductRow> rows, string fileName)
        {
            var file = await GeneratePdfFile(rows, fileName);
            var chunks = await ConvertToChunks(file);

            var fileChunks = ConvertToFormFiles(chunks, fileName);
            var doc = await repository.SaveFile(fileName);

            for (var i = 0; i < chunks.Count; ++i)
            {
                var chunk = await repository.SaveChunk(fileChunks[i], fileName, i);
                await supabase.UploadFileChunk(fileChunks[i], chunk);
            }

            return doc;

            //todo nastavi, treba uploadovati chunkove, spojiti sa providerom, documentom, exportom
        }
    }
}