namespace StoreManager.Application.Document.Service.Reader
{
    public interface IDocumentReaderFactory
    {
        IDocumentReaderService GetReader(string fileType);
    }
}
