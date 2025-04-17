using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Infrastructure.Invoice.Import.Repository
{
    public interface IImportRepository
    {
        Task<ImportModel> Create(ImportModel import);
        Task<ImportModel?> FindByDocumentId(Guid documentId);
        Task<(ICollection<ImportModel> Items, int TotalCount)> FindFiltered(string? componentInfo, Guid? providerId, DateOnly? dateIssued, int pageNumber, int pageSize);
        Task<List<ImportModel>> FindAll();
        Task<ImportModel?> FindById(Guid guid);
        Task<List<ImportModel>> FindByProviderId(Guid id);
        Task<int> CountImportsThisWeek();
        Task<int> FindCountForTheDate(DateOnly date);
    }
}
