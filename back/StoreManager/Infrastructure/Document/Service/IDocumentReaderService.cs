namespace StoreManager.Infrastructure.Document.Service
{
    public interface IDocumentReaderService
    {
        public string ExtractDataFromDocument(string filePath);
    }
}
