using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Document.Service
{
    public interface IDocumentService
    {
        Task<DocumentDownloadResponseDto> DownloadChunk(string fileName, int chunkIndex);

        Task UploadChunk(string provider, IFormFile file, string fileName, int chunkIndex, int totalChunks);

        Task<byte[]> GeneratePdfFile(List<ProductRow> rows, string fileName);
        Task<DocumentModel> UploadExport(List<ProductRow> rows, string fileName);
    }
}
