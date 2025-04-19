using StoreManager.Application.MechanicalComponent.DTO.Find;
using StoreManager.Application.MechanicalComponent.DTO.Info;
using StoreManager.Application.MechanicalComponent.DTO.Quantity;
using StoreManager.Application.MechanicalComponent.DTO.Search;
using StoreManager.Application.Shared;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.MechanicalComponent.Service
{
    public interface IMechanicalComponentService
    {
        Task<MechanicalComponentFindResponsesDto> FindByInvoiceId(string invoiceId);
        Task<PaginatedResult<MechanicalComponentSearchResponseDto>> FindFiltered(string? providerId,string? componentInfo, int pageNumber, int pageSize);
        Task<PaginatedResult<MechanicalComponentProductSearchResponseDto>> FindFilteredForProduct(string? providerId,string? componentInfo, int pageNumber, int pageSize);
        Task<MechanicalComponentInfoResponseDto> FindInfo(string componentId);
        Task<MechanicalComponentQuantitySumResponseDto> FindQuantitySum();
        Task<MechanicalComponentTopFiveQuantityResponsesDto> FindTopFiveInQuantity();
    }
}
