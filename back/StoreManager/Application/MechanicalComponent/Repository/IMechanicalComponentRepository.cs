using StoreManager.Domain.Document.Model;
using StoreManager.Infrastructure.MechanicalComponent.Model;

namespace StoreManager.Application.MechanicalComponent.Repository
{
    public interface IMechanicalComponentRepository
    {
        Task<MechanicalComponentModel?> FindByIdentifier(string identifier);
        Task<MechanicalComponentModel?> Create(MechanicalComponentModel component);
        Task<MechanicalComponentModel> CreateFromExtractionMetadata(ExtractionMetadata metadata);
        Task<List<MechanicalComponentModel>> CreateFromExtractionMetadata(List<ExtractionMetadata> metadata);
        Task<List<MechanicalComponentModel>> FindAll();

        Task<(ICollection<MechanicalComponentModel> Items, int TotalCount)> FindFiltered(Guid? providerId,
            string? componentInfo, int pageNumber, int pageSize);

        Task<(ICollection<MechanicalComponentModel> Items, int TotalCount)> FindFilteredForProduct(Guid? providerId,
            string? componentInfo, int pageNumber, int pageSize);

        Task<List<MechanicalComponentModel>> FindByInvoiceId(Guid invoiceId);
        Task<List<MechanicalComponentModel>> FindByProviderId(Guid id);
        Task<MechanicalComponentModel?> FindById(Guid componentGuid);
        Task<int> CountQuantity(MechanicalComponentModel componentModel);
        Task<int> FindQuantitySum();
        Task<List<MechanicalComponentModel>> FindTopFiveInQuantity();
        Task<List<MechanicalComponentModel>> FindByIds(List<Guid> componentIds);
    }
}