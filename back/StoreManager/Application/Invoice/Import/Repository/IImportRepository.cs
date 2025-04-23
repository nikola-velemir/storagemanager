using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Invoice.Import.Repository
{
    public interface IImportRepository
    {
        Task<ImportModel> Create(ImportModel import);
        Task<ImportModel?> FindByDocumentId(Guid documentId);

        Task<(ICollection<ImportModel> Items, int TotalCount)> FindFiltered(
            ISpecification<ImportModel> spec, string? componentInfo, Guid? providerId, DateOnly? dateIssued,
            int pageNumber, int pageSize);

        Task<List<ImportModel>> FindAll(ISpecification<ImportModel> spec);
        Task<ImportModel?> FindById(ISpecification<ImportModel> spec, Guid id);
        Task<List<ImportModel>> FindByProviderId(ISpecification<ImportModel> spec, Guid id);
        Task<int> CountImportsThisWeek();
        Task<int> FindCountForTheDateAsync(DateOnly date);
        Task<ImportItemModel?> FindByImportAndComponentIdAsync(Guid invoiceId, Guid componentId);
        Task UpdateAsync(ImportModel import);
        Task<double> FindTotalPrice();
        Task<double> FindSumForDate(DateOnly date);
    }
}