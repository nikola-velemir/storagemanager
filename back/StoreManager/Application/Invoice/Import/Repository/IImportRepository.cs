using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Invoice.Import.Repository
{
    public interface IImportRepository
    {
        Task<Domain.Invoice.Import.Model.Import> Create(Domain.Invoice.Import.Model.Import import);
        Task<Domain.Invoice.Import.Model.Import?> FindByDocumentId(Guid documentId);

        Task<(ICollection<Domain.Invoice.Import.Model.Import> Items, int TotalCount)> FindFiltered(
            ISpecification<Domain.Invoice.Import.Model.Import> spec, string? componentInfo, Guid? providerId, DateOnly? dateIssued,
            int pageNumber, int pageSize);

        Task<List<Domain.Invoice.Import.Model.Import>> FindAll(ISpecification<Domain.Invoice.Import.Model.Import> spec);
        Task<Domain.Invoice.Import.Model.Import?> FindById(ISpecification<Domain.Invoice.Import.Model.Import> spec, Guid id);
        Task<List<Domain.Invoice.Import.Model.Import>> FindByProviderId(ISpecification<Domain.Invoice.Import.Model.Import> spec, Guid id);
        Task<int> CountImportsThisWeek();
        Task<int> FindCountForTheDateAsync(DateOnly date);
        Task<ImportItemModel?> FindByImportAndComponentIdAsync(Guid invoiceId, Guid componentId);
        Task UpdateAsync(Domain.Invoice.Import.Model.Import import);
        Task<double> FindTotalPrice();
        Task<double> FindSumForDate(DateOnly date);
    }
}