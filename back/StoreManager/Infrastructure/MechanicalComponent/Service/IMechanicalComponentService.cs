using StoreManager.Infrastructure.MechanicalComponent.DTO;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.MechanicalComponent.Service
{
    public interface IMechanicalComponentService
    {
        Task<PaginatedResult<MechanicalComponentFindResponseDTO>> FindFiltered(int pageNumber, int pageSize);
    }
}
