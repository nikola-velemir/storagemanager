using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Product.Blueprint.Command;
using StoreManager.Application.Product.Blueprint.DTO;
using StoreManager.Application.Product.Blueprint.Repository;
using StoreManager.Application.Shared;
using StoreManager.Domain.Product.Blueprint.Specification;

namespace StoreManager.Application.Product.Blueprint.Handler;

public class FindFilteredProductsQueryHandler(IProductBlueprintRepository productBlueprintRepository)
    : IRequestHandler<FindFilteredProductBlueprintsQuery, Result<PaginatedResult<ProductSearchResponseDto>>>
{
    public async Task<Result<PaginatedResult<ProductSearchResponseDto>>> Handle(
        FindFilteredProductBlueprintsQuery request,
        CancellationToken cancellationToken)
    {
        DateOnly? date = null;
        if (DateOnly.TryParse(request.DateCreated, out var tempDate))
        {
            date = tempDate;
        }

        var products = await productBlueprintRepository.FindFilteredAsync(request.ProductInfo, date,
            request.PageNumber, request.PageSize, new ProductBlueprintsWithComponentsSpecification());
        return Result.Success(new PaginatedResult<ProductSearchResponseDto>
        {
            Items = products.Items.Select(p => new ProductSearchResponseDto(p.Id, p.Name, p.Identifier, p.DateCreated))
                .ToList(),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = products.TotalCount
        });
    }
}