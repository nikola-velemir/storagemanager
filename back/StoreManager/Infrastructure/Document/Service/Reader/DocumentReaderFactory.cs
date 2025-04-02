namespace StoreManager.Infrastructure.Document.Service.Reader
{
    public class DocumentReaderFactory : IDocumentReaderFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public DocumentReaderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDocumentReaderService GetReader(string fileType)
        {
            return fileType.ToLower() switch
            {
                "xlsx" => _serviceProvider.GetRequiredService<ExcelService>(),
                "pdf" => _serviceProvider.GetRequiredService<PDFService>(),
                _ => throw new NotImplementedException($"No reader for file type: {fileType}")
            };
        }
    }
}
