using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Invoice.DTO;

namespace StoreManager.Infrastructure.Invoice.Service
{
    public interface IInvoiceService
    {
        Task Create(Guid id, List<ExtractionMetadata> metadata);
        Task<InvoiceSearchResponsesDTO> FindAll();
    }
}
