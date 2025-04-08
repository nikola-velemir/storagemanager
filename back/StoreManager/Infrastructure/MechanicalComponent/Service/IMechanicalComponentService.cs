using StoreManager.Infrastructure.MechanicalComponent.DTO.Find;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Info;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Search;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.MechanicalComponent.Service
{
    public interface IMechanicalComponentService
    {
        Task<MechanicalComponentFindResponsesDTO> FindByInvoiceId(string invoiceId);
        Task<PaginatedResult<MechanicalComponentSearchResponseDTO>> FindFiltered(string? providerId,string? componentInfo, int pageNumber, int pageSize);
        Task<MechanicalComponentInfoResponseDTO> FindInfo(string componentId);
    }
}
