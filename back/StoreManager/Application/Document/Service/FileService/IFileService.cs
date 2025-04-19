using StoreManager.Domain.Document.Model;

namespace StoreManager.Application.Document.Service.FileService
{
    public interface IFileService
    {
        Task AppendChunk(IFormFile file, DocumentModel foundFile);
        Task DeleteAllChunks(DocumentModel file);
    }
}
