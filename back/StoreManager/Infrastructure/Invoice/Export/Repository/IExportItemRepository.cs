using StoreManager.Infrastructure.Document.Service;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.Invoice.Export.Repository;

public interface IExportItemRepository
{
    Task CreateFromProductRows(ExportModel export, List<ProductRow> productRows);
}