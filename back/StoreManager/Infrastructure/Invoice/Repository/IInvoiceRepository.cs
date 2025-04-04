using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Invoice.Model;

namespace StoreManager.Infrastructure.Invoice.Repository
{
    public interface IInvoiceRepository
    {
        Task<InvoiceModel> Create(InvoiceModel invoice);
        Task<InvoiceModel?> FindByDocumentId(Guid documentId);
        Task<(ICollection<InvoiceModel> Items, int TotalCount)> FindAllByDate(DateOnly date, int pageNumber, int pageSize);
        Task<List<InvoiceModel>> FindAll();
    }
}
