namespace StoreManager.Infrastructure.Document.Repository
{
    public  interface IDocumentRepository
    {
        Task<DocumentModel?> FindByName(string fileName);
        Task<DocumentModel> SaveFile(IFormFile file);
    }
}
