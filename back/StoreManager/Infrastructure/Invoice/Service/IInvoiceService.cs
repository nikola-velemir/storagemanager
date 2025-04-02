using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Invoice.Service
{
    public interface IInvoiceService
    {
        Task Create(Guid id, List<ExtractionMetadata> metadata);
    }
}
