using System.Text.RegularExpressions;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using Newtonsoft.Json;
using StoreManager.Application.BusinessPartner.Provider.DTO;
using StoreManager.Application.BusinessPartner.Provider.Repository;
using StoreManager.Application.Document.DTO;
using StoreManager.Application.Document.Repository;
using StoreManager.Application.Document.Service.FileService;
using StoreManager.Application.Document.Service.Reader;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Domain.BusinessPartner.Shared;
using StoreManager.Domain.Document.Model;
using StoreManager.Domain.Document.Service;
using StoreManager.Domain.Document.Specification;
using StoreManager.Domain.Document.Storage.Service;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Invoice.Import.Repository.Specification;
using StoreManager.Infrastructure.Invoice.Import.Service;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.Document.Service
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
            var invoice = await importRepository.FindById(new ImportWithDocument(),invoiceGuid);
            if (invoice is null)
            {
                throw new NotFoundException("Invoice not found");
            }

            var file = await repository.FindByNameAsync(new DocumentWithDocumentChunks(), invoice.Document.FileName) ??
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
                    provider = await providerRepository.FindByIdAsync(Guid.Parse(parsedProvider.ProviderId));
                    if (provider is null) throw new ArgumentNullException("provider is null");
                }
                else
                {
                    provider = await providerRepository.CreateAsync(new ProviderModel
                    {
                        Address = new Address("a","a","a",1,4),
                        Id = Guid.NewGuid(),
                        Type = BusinessPartnerType.Provider,
                        Name = parsedProvider.ProviderName,
                        PhoneNumber = parsedProvider.ProviderPhoneNumber
                    });
                }

                var parsedFileName = Regex.Replace(Path.GetFileNameWithoutExtension(fileName), @"[^a-zA-Z0-9]", "");
                var foundFile = await repository.FindByNameAsync(new DocumentWithDocumentChunks(), parsedFileName);
                if (foundFile == null)
                {
                    foundFile = await repository.SaveFileAsync(fileName);
                    var invoice = await importRepository.Create(new ImportModel
                    {
                        Provider = provider,
                        ProviderId = provider.Id,
                        DateIssued = DateOnly.FromDateTime(DateTime.UtcNow),
                        Document = foundFile,
                        DocumentId = foundFile.Id,
                        Id = Guid.NewGuid()
                    });
                    await providerRepository.AddInvoiceAsync(provider, invoice);
                }

                var savedChunk = await repository.SaveChunkAsync(file, fileName, chunkIndex);
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

        private static void AddTitle(Section section)
        {
            var title = section.AddParagraph("Export");
            title.Format.Font.Size = 22;
            title.Format.Font.Bold = true;
            title.Format.SpaceAfter = "1cm";
        }

        private static void AddExporterSection(BusinessPartnerModel partner, Section section)
        {
            var exporterSection =
                section.AddParagraph($"Exporter: \n{partner.Name}\n{partner.Address}\n{partner.PhoneNumber}");
            exporterSection.Format.Font.Size = 12;
            exporterSection.Format.SpaceAfter = "2cm";
        }

        private static Table AddTable(Section section)
        {
            var table = section.AddTable();
            table.Borders.Width = 0.75;
            table.Borders.Color = Colors.Black;
            table.Rows.LeftIndent = 0;
            return table;
        }

        private static void AddTableHeaders(Table table)
        {
            // Header row
            var headerRow = table.AddRow();
            headerRow.Shading.Color = Colors.LightGray;
            headerRow.Format.Font.Bold = true;
            headerRow.Format.Alignment = ParagraphAlignment.Center;
            headerRow.VerticalAlignment = VerticalAlignment.Center;

            headerRow.Cells[0].AddParagraph("Name");
            headerRow.Cells[1].AddParagraph("Identifier");
            headerRow.Cells[2].AddParagraph("Quantity");
            headerRow.Cells[3].AddParagraph("Price per piece");
        }

        private static void PopulateTable(List<ProductRow> rows, Table table)
        {
            foreach (var row in rows)
            {
                var r = table.AddRow();
                r.VerticalAlignment = VerticalAlignment.Center;

                r.Cells[0].AddParagraph(row.Name);
                r.Cells[1].AddParagraph(row.Identifier);
                r.Cells[2].AddParagraph(row.Quantity.ToString());
                r.Cells[3].AddParagraph(row.Price.ToString("C"));
            }
        }

        public Task<byte[]> GeneratePdfFile(BusinessPartnerModel partner, DateOnly dateIssued, List<ProductRow> rows,
            string fileName)
        {
            var doc = new MigraDoc.DocumentObjectModel.Document();
            var section = doc.AddSection();

            // Optional: Add title
            AddTitle(section);
            AddExporterSection(partner, section);
            var table = AddTable(section);


            // Define columns
            var columns = new[] { "3cm", "6cm", "3cm", "3cm" };
            foreach (var width in columns)
            {
                var col = table.AddColumn(width);
                col.Format.Alignment = ParagraphAlignment.Left;
            }

            AddTableHeaders(table);
            PopulateTable(rows, table);

            var renderer = new PdfDocumentRenderer(unicode: true)
            {
                Document = doc
            };

            renderer.RenderDocument();

            using var stream = new MemoryStream();
            renderer.PdfDocument.Save(stream, false);
            stream.Position = 0;

            return Task.FromResult(stream.ToArray());
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

        private static Task<List<byte[]>> ConvertToChunks(byte[] file)
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

            return Task.FromResult(chunks);
        }

        public async Task<DocumentModel> UploadExport(BusinessPartnerModel partner, DateOnly dateIssued,
            List<ProductRow> rows, string fileName)
        {
            var file = await GeneratePdfFile(partner, dateIssued, rows, fileName);
            var chunks = await ConvertToChunks(file);

            var fileChunks = ConvertToFormFiles(chunks, fileName);
            var doc = await repository.SaveFileAsync(fileName);

            for (var i = 0; i < chunks.Count; ++i)
            {
                var chunk = await repository.SaveChunkAsync(fileChunks[i], fileName, i);
                await supabase.UploadFileChunk(fileChunks[i], chunk);
            }

            return doc;
        }
    }
}