using StoreManager.Infrastructure.Product.Model;

namespace StoreManager.Infrastructure.Product.Repository;

public interface IProductRepository
{
    Task<ProductModel?> FindById(Guid id);
    Task<ProductModel> Create(ProductModel product);
    Task<(ICollection<ProductModel> Items, int TotalCount)> FindFiltered(string? productInfo, DateOnly? dateCreated, int pageNumber, int pageSize);
}