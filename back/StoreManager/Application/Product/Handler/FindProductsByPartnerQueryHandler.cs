using MediatR;
using StoreManager.Application.Product.Command;
using StoreManager.Application.Product.DTO;
using StoreManager.Application.Product.Repository;
using StoreManager.Infrastructure.Product.Model;

namespace StoreManager.Application.Product.Handler;

public class FindProductsByPartnerQueryHandler(IProductRepository productRepository)
    : IRequestHandler<FindProductsByPartnerQuery, List<ProductSearchResponseDto>>
{
    public async Task<List<ProductSearchResponseDto>> Handle(FindProductsByPartnerQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var exporterId))
            throw new InvalidCastException("Cant cast guid");

        var products = await productRepository.FindByExporterIdAsync(exporterId);

        return products.Select(p => new ProductSearchResponseDto(p.Id, p.Name, p.Identifier, p.DateCreated)).ToList();
    }
}