using StoreManager.Infrastructure.MechanicalComponent.DTO.Find;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Info;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Quantity;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Search;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.MechanicalComponent.Service
{
    public interface IMechanicalComponentService
    {
        Task<MechanicalComponentFindResponsesDto> FindByInvoiceId(string invoiceId);
        Task<PaginatedResult<MechanicalComponentSearchResponseDto>> FindFiltered(string? providerId,string? componentInfo, int pageNumber, int pageSize);
        Task<MechanicalComponentInfoResponseDto> FindInfo(string componentId);
        Task<MechanicalComponentQuantitySumResponseDto> FindQuantitySum();
        Task<MechanicalComponentTopFiveQuantityResponsesDto> FindTopFiveInQuantity();
    }
}
