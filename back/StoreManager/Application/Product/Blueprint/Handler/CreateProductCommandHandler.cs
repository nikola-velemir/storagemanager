using System.ComponentModel.DataAnnotations;
using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Application.Product.Blueprint.Command;
using StoreManager.Application.Product.Blueprint.DTO;
using StoreManager.Application.Product.Blueprint.Repository;
using StoreManager.Domain;
using StoreManager.Domain.Product.Blueprint.Model;

namespace StoreManager.Application.Product.Blueprint.Handler;

public class CreateProductCommandHandler(
    IUnitOfWork unitOfWork,
    IProductBlueprintRepository productBlueprintRepository,
    IMechanicalComponentRepository mechanicalComponentRepository) : IRequestHandler<CreateProductBlueprintCommand,Result>
{
    public async Task<Result> Handle(CreateProductBlueprintCommand request, CancellationToken cancellationToken)
    {
        ValidateRequest(request);
        
        var componentIds = ExtractComponentIds(request.Components);

        var productId = Guid.NewGuid();
        var components = await mechanicalComponentRepository.FindByIdsAsync(componentIds);
        var product = new ProductBlueprint
        {
            Identifier = request.Identifier,
            Components = new List<ProductBlueprintLineItem>(),
            Id = productId,
            Name = request.Name,
            Description = request.Description
        };

        product.Components = CreateComponentList(product, productId, request, components);

        await productBlueprintRepository.CreateAsync(product);
        
        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }

    private static void ValidateRequest(CreateProductBlueprintCommand request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.Name))
            errors.Add("Product name is required.");

        if (string.IsNullOrWhiteSpace(request.Identifier))
            errors.Add("Identifier is required.");
        else if (request.Identifier.Length < 6)
            errors.Add("Identifier must not be under 6 characters.");

        if (string.IsNullOrWhiteSpace(request.Description))
            errors.Add("Description is required.");

        var invalidQuantities = request.Components.Where(c => c.Quantity <= 0).ToList();
        if (invalidQuantities.Count != 0)
            errors.Add("All components must have a quantity greater than zero.");

        if (errors.Count != 0)
            throw new ValidationException(string.Join(" ", errors));
    }

    private static List<ProductBlueprintLineItem> CreateComponentList(ProductBlueprint product, Guid productId,
        CreateProductBlueprintCommand request, List<Domain.MechanicalComponent.Model.MechanicalComponent> components)
    {
        return components.Select(c =>
        {
            var dtoComponent = request.Components.First(dc => Guid.TryParse(dc.Id, out var guid) && guid.Equals(c.Id));
            return new ProductBlueprintLineItem
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