using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Invoice.Model;

namespace StoreManager.Infrastructure.Invoice.Repository
{
    public interface IInvoiceRepository
    {
        Task<InvoiceModel> Save(InvoiceModel invoice);
        Task<(ICollection<InvoiceModel> Items, int TotalCount)> FindAllByDate(DateOnly date, int pageNumber, int pageSize);
    }
}
