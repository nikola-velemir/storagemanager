namespace StoreManager.Infrastructure.Document.Service.Reader
{
    public interface IDocumentReaderFactory
    {
        IDocumentReaderService GetReader(string fileType);
    }
}
