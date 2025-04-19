using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Application.Invoice.Import.Repository
{
    public interface IImportItemRepository
    {
        Task<ImportItemModel> Create(ImportItemModel importItem);
        Task<ImportItemModel?> FindByImportAndComponentId(Guid invoiceId, Guid componentId);
        Task<double> FindSumForDate(DateOnly date);
        Task<double> FindTotalPrice();
    }
}
