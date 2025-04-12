using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.Product.DTO;
using StoreManager.Infrastructure.Product.Model;
using StoreManager.Infrastructure.Product.Repository;

namespace StoreManager.Infrastructure.Product.Service;

public class ProductService(IProductRepository productRepository, IMechanicalComponentRepository mechanicalComponentRepository) : IProductService
{
    public async Task CreateProduct(ProductCreateRequestDto dto)
    {
        var componentIds = dto.Components
            .Select(c => Guid.TryParse(c.Id, out var guid) ? (Guid?)guid : null)
            .Where(g => g.HasValue)
            .Select(g => g.Value)
            .ToList();

        var productId = Guid.NewGuid();
        var components = await mechanicalComponentRepository.FindByIds(componentIds);
        var product = new ProductModel
        {
            Components = new List<ProductComponentsModel>(),
            Id = productId,
            Name = dto.Name,
            Description = dto.Description
        };

        product.Components = components.Select(c =>
        {
            var dtoComponent = dto.Components.First(dc => Guid.TryParse(dc.Id, out var guid) && guid.Equals(c.Id));
            return new ProductComponentsModel
            {
                Component = c,
                Product = product,
                ComponentId = c.Id,
                ProductId = productId,
                UsedQuantity = dtoComponent.Quantity
            };
        }).ToList();

       await productRepository.Create(product);
    }
}