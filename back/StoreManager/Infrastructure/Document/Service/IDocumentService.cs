using StoreManager.Infrastructure.BusinessPartner.Base.Model;
using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Invoice.Base;

namespace StoreManager.Infrastructure.Document.Service
{
    public interface IDocumentService
    {
        Task<DocumentDownloadResponseDto> DownloadChunk(string fileName, int chunkIndex);

        Task UploadChunk(string provider, IFormFile file, string fileName, int chunkIndex, int totalChunks);

        Task<byte[]> GeneratePdfFile(BusinessPartnerModel partner,DateOnly dateIssued, List<ProductRow> rows, string fileName);
        Task<DocumentModel> UploadExport(BusinessPartnerModel partner,DateOnly dateIssued, List<ProductRow> rows, string fileName);
    }
}