using StoreManager.Infrastructure.Invoice.Model;

namespace StoreManager.Infrastructure.Invoice.Repository
{
    public interface IInvoiceItemRepository
    {
        Task<InvoiceItemModel> Create(InvoiceItemModel invoiceItem);
        Task<InvoiceItemModel?> FindByInvoiceAndComponentId(Guid invoiceId, Guid componentId);
        Task<double> FindSumForDate(DateOnly date);
        Task<double> FindTotalPrice();
    }
}
