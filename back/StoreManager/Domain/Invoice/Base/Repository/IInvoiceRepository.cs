using StoreManager.Domain.Invoice.Base.Model;
using StoreManager.Infrastructure.Invoice.Base;

namespace StoreManager.Domain.Invoice.Base.Repository;

public interface IInvoiceRepository
{
    Task<InvoiceModel?> FindById(Guid id);
    Task<List<InvoiceModel>> FindByPartnerId(Guid partnerId);
}