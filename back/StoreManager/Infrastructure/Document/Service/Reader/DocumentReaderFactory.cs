namespace StoreManager.Infrastructure.Document.Service.Reader
{
    public class DocumentReaderFactory(IServiceProvider serviceProvider) : IDocumentReaderFactory
    {
        public IDocumentReaderService GetReader(string fileType)
        {
            return fileType.ToLower() switch
            {
                "xlsx" => serviceProvider.GetRequiredService<ExcelService>(),
                "pdf" => serviceProvider.GetRequiredService<PdfService>(),
                _ => throw new NotImplementedException($"No reader for file type: {fileType}")
            };
        }
    }
}
