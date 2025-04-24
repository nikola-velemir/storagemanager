using StoreManager.Domain.Document.Model;
using StoreManager.Domain.MechanicalComponent.Model;

namespace StoreManager.Application.MechanicalComponent.Repository
{
    public interface IMechanicalComponentRepository
    {
        Task<Domain.MechanicalComponent.Model.MechanicalComponent?> FindByIdentifierAsync(string identifier);
        Task<Domain.MechanicalComponent.Model.MechanicalComponent?> CreateAsync(Domain.MechanicalComponent.Model.MechanicalComponent component);
        Task<Domain.MechanicalComponent.Model.MechanicalComponent> CreateFromExtractionMetadataAsync(ExtractionMetadata metadata);
        Task<List<Domain.MechanicalComponent.Model.MechanicalComponent>> CreateFromExtractionMetadataAsync(List<ExtractionMetadata> metadata);
        Task<List<Domain.MechanicalComponent.Model.MechanicalComponent>> FindAllAsync();

        Task<(ICollection<Domain.MechanicalComponent.Model.MechanicalComponent> Items, int TotalCount)> FindFilteredAsync(Guid? providerId,
            string? componentInfo, int pageNumber, int pageSize);

        Task<(ICollection<Domain.MechanicalComponent.Model.MechanicalComponent> Items, int TotalCount)> FindFilteredForProductAsync(Guid? providerId,
            string? componentInfo, int pageNumber, int pageSize);

        Task<List<Domain.MechanicalComponent.Model.MechanicalComponent>> FindByInvoiceIdAsync(Guid invoiceId);
        Task<List<Domain.MechanicalComponent.Model.MechanicalComponent>> FindByProviderIdAsync(Guid id);
        Task<Domain.MechanicalComponent.Model.MechanicalComponent?> FindByIdAsync(Guid componentGuid);
        Task<int> CountQuantityAsync(Domain.MechanicalComponent.Model.MechanicalComponent component);
        Task<int> FindQuantitySumAsync();
        Task<List<Domain.MechanicalComponent.Model.MechanicalComponent>> FindTopFiveInQuantityAsync();
        Task<List<Domain.MechanicalComponent.Model.MechanicalComponent>> FindByIdsAsync(List<Guid> componentIds);
    }
}