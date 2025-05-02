using StoreManager.Application.Product.Blueprint.DTO;
using StoreManager.Application.Shared;

namespace StoreManager.Domain.Product.Blueprint.Service;

public interface IProductBlueprintService
{
    Task CreateProduct(ProductCreateRequestDto dto);

    Task<PaginatedResult<ProductSearchResponseDto>> FindFiltered(string? productInfo, string? dateCreated,
        int pageNumber,
        int pageSize);

    Task<ProductInfoResponseDto> FindProductInfo(string id);
}