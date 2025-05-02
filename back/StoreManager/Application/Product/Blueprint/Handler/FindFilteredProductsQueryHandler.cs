using MediatR;
using StoreManager.Application.Product.Blueprint.Command;
using StoreManager.Application.Product.Blueprint.DTO;
using StoreManager.Application.Product.Blueprint.Repository;
using StoreManager.Application.Shared;

namespace StoreManager.Application.Product.Blueprint.Handler;

public class FindFilteredProductsQueryHandler(IProductBlueprintRepository productBlueprintRepository)
    : IRequestHandler<FindFilteredProductBlueprintsQuery, PaginatedResult<ProductSearchResponseDto>>
{
    public async Task<PaginatedResult<ProductSearchResponseDto>> Handle(FindFilteredProductBlueprintsQuery request,
        CancellationToken cancellationToken)
    {
        DateOnly? date = null;
        if (DateOnly.TryParse(request.DateCreated, out var tempDate))
        {
            date = tempDate;
        }

        var products = await productBlueprintRepository.FindFilteredAsync(request.ProductInfo, date,
            request.PageNumber, request.PageSize);
        return new PaginatedResult<ProductSearchResponseDto>
        {
            Items = products.Items.Select(p => new ProductSearchResponseDto(p.Id, p.Name, p.Identifier, p.DateCreated))
                .ToList(),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = products.TotalCount
        };
    }
}