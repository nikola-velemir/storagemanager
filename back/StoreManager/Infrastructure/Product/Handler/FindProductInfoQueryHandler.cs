using MediatR;
using StoreManager.Infrastructure.MiddleWare.Exceptions;
using StoreManager.Infrastructure.Product.Command;
using StoreManager.Infrastructure.Product.DTO;
using StoreManager.Infrastructure.Product.Repository;

namespace StoreManager.Infrastructure.Product.Handler;

public class FindProductInfoQueryHandler(IProductRepository productRepository)
    : IRequestHandler<FindProductInfoQuery, ProductInfoResponseDto>
{
    public async Task<ProductInfoResponseDto> Handle(FindProductInfoQuery request, CancellationToken cancellationToken)
    {
        Guid? productGuid = null;
        if (!Guid.TryParse(request.Id, out _))
        {
            throw new InvalidCastException("Could not cast guid");
        }

        productGuid = Guid.Parse(request.Id);
        var product = await productRepository.FindById(productGuid.Value);
        if (product is null)
        {
            throw new NotFoundException("Product not found");
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
                .ToList());
    }
}