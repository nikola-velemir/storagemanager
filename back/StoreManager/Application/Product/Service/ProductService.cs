using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Application.Product.DTO;
using StoreManager.Application.Product.Repository;
using StoreManager.Application.Shared;
using StoreManager.Infrastructure.MiddleWare.Exceptions;
using StoreManager.Infrastructure.Product.Model;
using StoreManager.Infrastructure.Product.Service;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Product.Service;

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
        var components = await mechanicalComponentRepository.FindByIdsAsync(componentIds);
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

        await productRepository.CreateAsync(product);
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

        var products = await productRepository.FindFilteredAsync(productInfo, date, pageNumber, pageSize);
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
        var product = await productRepository.FindByIdAsync(productGuid.Value);
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
                        p.Component.Id,
                        p.Component.Name,
                        p.Component.Identifier,
                        p.UsedQuantity))
                .ToList(),
            product.Exports.Select(e =>
            {
                var exporter = e.Export.Exporter;
                var exporterDto = new FindProductInfoExporterResponseDto(exporter.Id, exporter.Name, exporter.Address, exporter.PhoneNumber);
                return new ProductInfoExportResponseDto(e.ExportId, e.Export.DateIssued,exporterDto);
            }).ToList());
    }
}