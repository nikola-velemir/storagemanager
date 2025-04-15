using StoreManager.Infrastructure.Document.Model;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace StoreManager.Infrastructure.Document.Service.Reader
{
    public sealed class PdfService : IDocumentReaderService
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
                                 int.Parse(segments[^3]),
                                 double.Parse(segments[^2].Replace('.', ',')));

                            tableData.Add(obj);
                        }
                    }
                }
            }
            return tableData;
        }
    }
}
