namespace StoreManager.Infrastructure.Invoice.Base.Repository;

public interface IInvoiceRepository
{
    Task<InvoiceModel?> FindById(Guid id);
}