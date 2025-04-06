using StoreManager.Infrastructure.Invoice.Model;

namespace StoreManager.Infrastructure.Invoice.Repository
{
    public interface IInvoiceItemRepository
    {
        public Task<InvoiceItemModel> Create(InvoiceItemModel invoiceItem);
        public Task<InvoiceItemModel?> FindByInvoiceAndComponentId(Guid invoiceId, Guid componentId);
    }
}
