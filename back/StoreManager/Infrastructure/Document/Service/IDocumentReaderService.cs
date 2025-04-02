using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Document.Service
{
    public interface IDocumentReaderService
    {
        public List<ExtractionMetadata> ExtractDataFromDocument(string filePath);
    }
}
