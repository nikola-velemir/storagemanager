using StoreManager.Infrastructure.Product.DTO;

namespace StoreManager.Infrastructure.Product.Service;

public interface IProductService
{
    Task CreateProduct(ProductCreateRequestDto dto);

}