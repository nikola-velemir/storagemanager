using StoreManager.Domain.Product.Blueprint.Model;

namespace StoreManager.Application.Product.Blueprint.Repository;

public interface IProductBlueprintRepository
{
    Task<ProductBlueprint?> FindByIdAsync(Guid id);
    Task<ProductBlueprint> CreateAsync(ProductBlueprint product);
    Task<(ICollection<ProductBlueprint> Items, int TotalCount)> FindFilteredAsync(string? productInfo, DateOnly? dateCreated, int pageNumber, int pageSize);
    Task<List<ProductBlueprint>> FindByInvoiceIdAsync(Guid invoiceId);
    Task<List<ProductBlueprint>> FindByExporterIdAsync(Guid exporterId);
}