using StoreManager.Application.Product.DTO;
using StoreManager.Application.Shared;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Product.Service;

public interface IProductService
{
    Task CreateProduct(ProductCreateRequestDto dto);

    Task<PaginatedResult<ProductSearchResponseDto>> FindFiltered(string? productInfo, string? dateCreated,
        int pageNumber,
        int pageSize);

    Task<ProductInfoResponseDto> FindProductInfo(string id);
}