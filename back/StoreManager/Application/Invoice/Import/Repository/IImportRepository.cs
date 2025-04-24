using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Invoice.Import.Repository
{
    public interface IImportRepository
    {
        Task<Infrastructure.Invoice.Import.Model.Import> Create(Infrastructure.Invoice.Import.Model.Import import);
        Task<Infrastructure.Invoice.Import.Model.Import?> FindByDocumentId(Guid documentId);

        Task<(ICollection<Infrastructure.Invoice.Import.Model.Import> Items, int TotalCount)> FindFiltered(
            ISpecification<Infrastructure.Invoice.Import.Model.Import> spec, string? componentInfo, Guid? providerId, DateOnly? dateIssued,
            int pageNumber, int pageSize);

        Task<List<Infrastructure.Invoice.Import.Model.Import>> FindAll(ISpecification<Infrastructure.Invoice.Import.Model.Import> spec);
        Task<Infrastructure.Invoice.Import.Model.Import?> FindById(ISpecification<Infrastructure.Invoice.Import.Model.Import> spec, Guid id);
        Task<List<Infrastructure.Invoice.Import.Model.Import>> FindByProviderId(ISpecification<Infrastructure.Invoice.Import.Model.Import> spec, Guid id);
        Task<int> CountImportsThisWeek();
        Task<int> FindCountForTheDateAsync(DateOnly date);
        Task<ImportItemModel?> FindByImportAndComponentIdAsync(Guid invoiceId, Guid componentId);
        Task UpdateAsync(Infrastructure.Invoice.Import.Model.Import import);
        Task<double> FindTotalPrice();
        Task<double> FindSumForDate(DateOnly date);
    }
}