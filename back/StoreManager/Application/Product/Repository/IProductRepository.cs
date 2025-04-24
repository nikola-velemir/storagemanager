using StoreManager.Domain.Product.Model;

namespace StoreManager.Application.Product.Repository;

public interface IProductRepository
{
    Task<ProductBlueprint?> FindByIdAsync(Guid id);
    Task<ProductBlueprint> CreateAsync(ProductBlueprint product);
    Task<(ICollection<ProductBlueprint> Items, int TotalCount)> FindFilteredAsync(string? productInfo, DateOnly? dateCreated, int pageNumber, int pageSize);
    Task<List<ProductBlueprint>> FindByInvoiceIdAsync(Guid invoiceId);
    Task<List<ProductBlueprint>> FindByExporterIdAsync(Guid exporterId);
}