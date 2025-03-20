using StoreManager.Infrastructure.Document.DTO;

namespace StoreManager.Infrastructure.Document.Service
{
    public interface IDocumentService
    {
        Task<DocumentDownloadResponseDTO> DownloadFile(string fileName);

        Task UploadFile(IFormFile file);
    }
}
