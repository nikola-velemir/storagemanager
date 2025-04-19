using StoreManager.Application.Document.DTO;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.Document.Model;

namespace StoreManager.Domain.Document.Service
{
    public interface IDocumentService
    {
        Task<DocumentDownloadResponseDto> DownloadChunk(string fileName, int chunkIndex);

        Task UploadChunk(string provider, IFormFile file, string fileName, int chunkIndex, int totalChunks);

        Task<byte[]> GeneratePdfFile(BusinessPartnerModel partner,DateOnly dateIssued, List<ProductRow> rows, string fileName);
        Task<DocumentModel> UploadExport(BusinessPartnerModel partner,DateOnly dateIssued, List<ProductRow> rows, string fileName);
    }
}