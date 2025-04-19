using StoreManager.Domain.Document.Service;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Application.Invoice.Export.Repository;

public interface IExportItemRepository
{
    Task CreateFromProductRows(ExportModel export, List<ProductRow> productRows);
}