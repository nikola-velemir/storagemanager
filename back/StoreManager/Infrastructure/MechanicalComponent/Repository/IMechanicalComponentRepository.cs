using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.MechanicalComponent.Model;

namespace StoreManager.Infrastructure.MechanicalComponent.Repository
{
    public interface IMechanicalComponentRepository
    {
        Task<MechanicalComponentModel?> FindByIdentifier(string identifier);
        Task<MechanicalComponentModel?> Create(MechanicalComponentModel component);
        Task<MechanicalComponentModel> CreateFromExtractionMetadata(ExtractionMetadata metadata);
        Task<List<MechanicalComponentModel>> CreateFromExtractionMetadata(List<ExtractionMetadata> metadata);

    }
}
