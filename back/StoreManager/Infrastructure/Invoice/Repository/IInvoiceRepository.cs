using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Invoice.Model;

namespace StoreManager.Infrastructure.Invoice.Repository
{
    public interface IInvoiceRepository
    {
        Task<InvoiceModel> Create(InvoiceModel invoice);
        Task<InvoiceModel?> FindByDocumentId(Guid documentId);
        Task<(ICollection<InvoiceModel> Items, int TotalCount)> FindFiltered(Guid? providerId, DateOnly? dateIssued, int pageNumber, int pageSize);
        Task<List<InvoiceModel>> FindAll();
    }
}
