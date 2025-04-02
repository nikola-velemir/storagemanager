using ClosedXML.Excel;
using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Document.Service.Reader
{
    public class ExcelService : IDocumentReaderService
    {
        public List<ExtractionMetadata> ExtractDataFromDocument(string filePath)
        {
            var datalist = new List<ExtractionMetadata>();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);

                var rowCount = worksheet.RowsUsed().Count();
                var colCount = worksheet.ColumnsUsed().Count();

                for (int row = 2; row <= rowCount; row++)
                {
                    var identifier = worksheet.Cell(row, 1).Value.ToString();
                    var name = worksheet.Cell(row, 2).Value.ToString();
                    var price = double.Parse(worksheet.Cell(row, 4).Value.ToString());
                    var quantity = int.Parse(worksheet.Cell(row, 3).Value.ToString());

                    datalist.Add(new ExtractionMetadata(identifier, name, quantity, price));
                }
            }
            return datalist;
        }
    }
}
