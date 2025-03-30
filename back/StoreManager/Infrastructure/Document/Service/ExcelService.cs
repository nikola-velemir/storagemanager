
using ClosedXML.Excel;
using Newtonsoft.Json;

namespace StoreManager.Infrastructure.Document.Service
{
    public class ExcelService : IDocumentReaderService
    {
        public string ExtractDataFromDocument(string filePath) {
            var datalist = new List<Dictionary<string, string>>();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Get the first worksheet

                var rowCount = worksheet.RowsUsed().Count();
                var colCount = worksheet.ColumnsUsed().Count();

                // Loop through the rows
                var headers = new List<string>();

                for(int col = 1; col<= colCount; col++)
                {
                    headers.Add(worksheet.Cell(1, col).Value.ToString());
                }
                
                for (int row = 2; row <= rowCount; row++)
                {
                    var rowData = new Dictionary<string, string>();
                    for (int col = 1; col <= colCount; col++)
                    {
                        rowData[headers[col - 1]] = worksheet.Cell(row, col).Value.ToString();
                    }
                    datalist.Add(rowData);
                }

            }

            return JsonConvert.SerializeObject(datalist, Formatting.Indented);
        }
    }
}
