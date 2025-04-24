using StoreManager.Domain.Invoice.Base.Model;
using StoreManager.Infrastructure.Invoice.Base;

namespace StoreManager.Domain.Invoice.Base.Repository;

public interface IInvoiceRepository
{
    Task<Model.Invoice?> FindById(Guid id);
    Task<List<Model.Invoice>> FindByPartnerId(Guid partnerId);
}