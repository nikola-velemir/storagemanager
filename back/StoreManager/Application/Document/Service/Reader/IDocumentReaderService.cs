using StoreManager.Domain.Document.Model;

namespace StoreManager.Application.Document.Service.Reader
{
    public interface IDocumentReaderService
    {
        public List<ExtractionMetadata> ExtractDataFromDocument(string filePath);
    }
}
