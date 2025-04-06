using StoreManager.Infrastructure.MechanicalComponent.DTO;

namespace StoreManager.Infrastructure.MechanicalComponent.Service
{
    public interface IMechanicalComponentService
    {
        Task<MechanicalComponentSearchResponsesDTO> FindFiltered(int pageNumber, int pageSize);
    }
}
