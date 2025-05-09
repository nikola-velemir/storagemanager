using StoreManager.Domain.Product.Blueprint.Model;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Product.Blueprint.Repository;

public interface IProductBlueprintRepository
{
    Task<ProductBlueprint?> FindByIdAsync(Guid id);
    Task<ProductBlueprint> CreateAsync(ProductBlueprint product);

    Task<(ICollection<ProductBlueprint> Items, int TotalCount)> FindFilteredAsync(string? productInfo,
        DateOnly? dateCreated, int pageNumber, int pageSize, ISpecification<ProductBlueprint> spec);

    Task<List<ProductBlueprint>> FindByInvoiceIdAsync(Guid invoiceId);
    Task<List<ProductBlueprint>> FindByExporterIdAsync(Guid exporterId);
}