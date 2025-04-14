using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.MiddleWare.Exceptions;
using StoreManager.Infrastructure.Product.DTO;
using StoreManager.Infrastructure.Product.Model;
using StoreManager.Infrastructure.Product.Repository;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Product.Service;

public class ProductService(
    IProductRepository productRepository,
    IMechanicalComponentRepository mechanicalComponentRepository) : IProductService
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
            Identifier = dto.Identifier,
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

    public async Task<PaginatedResult<ProductSearchResponseDto>> FindFiltered(string? productInfo, string? dateCreated,
        int pageNumber,
        int pageSize)
    {
        DateOnly? date = null;
        if (DateOnly.TryParse(dateCreated, out var tempDate))
        {
            date = tempDate;
        }

        var products = await productRepository.FindFiltered(productInfo, date, pageNumber, pageSize);
        return new PaginatedResult<ProductSearchResponseDto>
        {
            Items = products.Items.Select(p => new ProductSearchResponseDto(p.Id, p.Name, p.Identifier, p.DateCreated))
                .ToList(),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = products.TotalCount
        };
    }

    public async Task<ProductInfoResponseDto> FindProductInfo(string id)
    {
        Guid? productGuid = null;
        if (!Guid.TryParse(id, out _))
        {
            throw new InvalidCastException("Could not cast guid");
        }

        productGuid = Guid.Parse(id);
        var product = await productRepository.FindById(productGuid.Value);
        if (product is null)
        {
            throw new NotFoundException("Product not found!");
        }

        return new ProductInfoResponseDto(
            product.Name,
            product.Description,
            product.Identifier,
            product.DateCreated,
            product.Components.Select(p =>
                    new ProductInfoComponentResponseDto(
                        p.ComponentId,
                        p.Component.Name,
                        p.Component.Identifier,
                        p.UsedQuantity))
                .ToList());
    }
}