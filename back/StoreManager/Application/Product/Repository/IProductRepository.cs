using StoreManager.Infrastructure.Product.Model;

namespace StoreManager.Application.Product.Repository;

public interface IProductRepository
{
    Task<ProductModel?> FindByIdAsync(Guid id);
    Task<ProductModel> CreateAsync(ProductModel product);
    Task<(ICollection<ProductModel> Items, int TotalCount)> FindFilteredAsync(string? productInfo, DateOnly? dateCreated, int pageNumber, int pageSize);
    Task<List<ProductModel>> FindByInvoiceIdAsync(Guid invoiceId);
    Task<List<ProductModel>> FindByExporterIdAsync(Guid exporterId);
}