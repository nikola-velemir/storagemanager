using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.Model;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.Product.Command;
using StoreManager.Infrastructure.Product.DTO;
using StoreManager.Infrastructure.Product.Model;
using StoreManager.Infrastructure.Product.Repository;

namespace StoreManager.Infrastructure.Product.Handler;

public class CreateProductCommandHandler(
    IProductRepository productRepository,
    IMechanicalComponentRepository mechanicalComponentRepository) : IRequestHandler<CreateProductCommand>
{
    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var componentIds = ExtractComponentIds(request.Components);

        var productId = Guid.NewGuid();
        var components = await mechanicalComponentRepository.FindByIds(componentIds);
        var product = new ProductModel
        {
            Identifier = request.Identifier,
            Components = new List<ProductComponentsModel>(),
            Id = productId,
            Name = request.Name,
            Description = request.Description
        };

        product.Components = CreateComponentList(product, productId, request, components);

        await productRepository.Create(product);
        return Unit.Value;
    }

    private static List<ProductComponentsModel> CreateComponentList(ProductModel product, Guid productId,
        CreateProductCommand request, List<MechanicalComponentModel> components)
    {
        return components.Select(c =>
        {
            var dtoComponent = request.Components.First(dc => Guid.TryParse(dc.Id, out var guid) && guid.Equals(c.Id));
            return new ProductComponentsModel
            {
                Component = c,
                Product = product,
                ComponentId = c.Id,
                ProductId = productId,
                UsedQuantity = dtoComponent.Quantity
            };
        }).ToList();
    }

    private static List<Guid> ExtractComponentIds(List<ProductCreateRequestComponentDto> components)
    {
        return components
            .Select(c => Guid.TryParse(c.Id, out var guid) ? (Guid?)guid : null)
            .Where(g => g.HasValue)
            .Select(g => g.Value)
            .ToList();
    }
}