using MediatR;
using StoreManager.Application.Product.Blueprint.Command;
using StoreManager.Application.Product.Blueprint.DTO;
using StoreManager.Application.Product.Blueprint.Repository;

namespace StoreManager.Application.Product.Blueprint.Handler;

public class FindProductsByPartnerQueryHandler(IProductBlueprintRepository productBlueprintRepository)
    : IRequestHandler<FindProductBlueprintsByPartnerQuery, List<ProductSearchResponseDto>>
{
    public async Task<List<ProductSearchResponseDto>> Handle(FindProductBlueprintsByPartnerQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var exporterId))
            throw new InvalidCastException("Cant cast guid");

        var products = await productBlueprintRepository.FindByExporterIdAsync(exporterId);

        return products.Select(p => new ProductSearchResponseDto(p.Id, p.Name, p.Identifier, p.DateCreated)).ToList();
    }
}