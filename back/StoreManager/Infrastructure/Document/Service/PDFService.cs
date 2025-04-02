using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Document.Service;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace StoreManager.Tests.Document.Service
{
    public class PDFService : IDocumentReaderService
    {
        public List<ExtractionMetadata> ExtractDataFromDocument(string filePath)
        {
            var tableData = new List<ExtractionMetadata>();

            using (var document = PdfDocument.Open(filePath))
            {
                foreach (var page in document.GetPages())
                {
                    var text = ContentOrderTextExtractor.GetText(page);

                    string[] lines = text.Split(["\r\n", "\r", "\n"], StringSplitOptions.None);
                    foreach (var line in lines)
                    {
                        if (line.StartsWith("MC-"))
                        {
                            var segments = line.Split(' ');

                            var obj = new ExtractionMetadata(segments[0],
                                 string.Join(" ", segments, 1, segments.Length - 4),
                                 Int32.Parse(segments[^3]),
                                 Double.Parse(segments[^2].Replace('.', ',')));

                            tableData.Add(obj);
                        }
                    }
                }
            }
            return tableData;
        }
    }
}
