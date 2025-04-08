using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Document.Service.FileService
{
    public interface IFileService
    {
        Task AppendChunk(IFormFile file, DocumentModel foundFile);
        Task DeleteAllChunks(DocumentModel file);
    }
}
