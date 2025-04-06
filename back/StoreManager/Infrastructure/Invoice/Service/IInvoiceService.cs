using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Invoice.DTO;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Service
{
    public interface IInvoiceService
    {
        Task Create(Guid id, List<ExtractionMetadata> metadata);
        Task<InvoiceSearchResponsesDTO> FindFilteredInvoices(string? providerId, string? date, int pageNumber, int pageSize);
    }
}
