using StoreManager.Domain.Document.Model;
using StoreManager.Infrastructure.MechanicalComponent.Model;

namespace StoreManager.Application.MechanicalComponent.Repository
{
    public interface IMechanicalComponentRepository
    {
        Task<MechanicalComponentModel?> FindByIdentifierAsync(string identifier);
        Task<MechanicalComponentModel?> CreateAsync(MechanicalComponentModel component);
        Task<MechanicalComponentModel> CreateFromExtractionMetadataAsync(ExtractionMetadata metadata);
        Task<List<MechanicalComponentModel>> CreateFromExtractionMetadataAsync(List<ExtractionMetadata> metadata);
        Task<List<MechanicalComponentModel>> FindAllAsync();

        Task<(ICollection<MechanicalComponentModel> Items, int TotalCount)> FindFilteredAsync(Guid? providerId,
            string? componentInfo, int pageNumber, int pageSize);

        Task<(ICollection<MechanicalComponentModel> Items, int TotalCount)> FindFilteredForProductAsync(Guid? providerId,
            string? componentInfo, int pageNumber, int pageSize);

        Task<List<MechanicalComponentModel>> FindByInvoiceIdAsync(Guid invoiceId);
        Task<List<MechanicalComponentModel>> FindByProviderIdAsync(Guid id);
        Task<MechanicalComponentModel?> FindByIdAsync(Guid componentGuid);
        Task<int> CountQuantityAsync(MechanicalComponentModel componentModel);
        Task<int> FindQuantitySumAsync();
        Task<List<MechanicalComponentModel>> FindTopFiveInQuantityAsync();
        Task<List<MechanicalComponentModel>> FindByIdsAsync(List<Guid> componentIds);
    }
}