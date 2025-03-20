namespace StoreManager.Infrastructure.Document.Repository
{
    public  interface IDocumentRepository
    {
        Task<DocumentModel> SaveFile(IFormFile file);
    }
}
