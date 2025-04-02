using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Document.Service.Reader
{
    public interface IDocumentReaderService
    {
        public List<ExtractionMetadata> ExtractDataFromDocument(string filePath);
    }
}
