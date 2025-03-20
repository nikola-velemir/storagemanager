namespace StoreManager.Infrastructure.Document.Service
{
    public interface IDocumentService
    {
        Task UploadFile(IFormFile file);
    }
}
