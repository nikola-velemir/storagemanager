using StoreManager.Domain.Document.Model;

namespace StoreManager.Application.Document.Service.FileService
{
    public interface IFileService
    {
        Task AppendChunk(IFormFile file, Domain.Document.Model.Document foundFile);
        Task DeleteAllChunks(Domain.Document.Model.Document file);
    }
}
