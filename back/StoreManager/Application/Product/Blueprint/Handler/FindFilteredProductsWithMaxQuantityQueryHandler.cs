using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Product.Blueprint.Command;
using StoreManager.Application.Product.Blueprint.DTO;
using StoreManager.Application.Product.Blueprint.Repository;
using StoreManager.Application.Shared;
using StoreManager.Domain.Product.Blueprint.Specification;

namespace StoreManager.Application.Product.Blueprint.Handler;

public class FindFilteredProductsWithMaxQuantityQueryHandler(IProductBlueprintRepository productBlueprintRepository)
    : IRequestHandler<FindFilteredProductBlueprintWithMaxQuantityQuery,
        Result<PaginatedResult<ProductSearchWithQuantityResponseDto>>>
{
    public async Task<Result<PaginatedResult<ProductSearchWithQuantityResponseDto>>> Handle(
        FindFilteredProductBlueprintWithMaxQuantityQuery request, CancellationToken cancellationToken)
    {
        DateOnly? date = null;
        if (DateOnly.TryParse(request.DateCreated, out var tempDate))
        {
            date = tempDate;
        }

        var products = await productBlueprintRepository.FindFilteredAsync(request.ProductInfo, date,
            request.PageNumber, request.PageSize, new ProductBlueprintsWithLineItemsSpecification());
        var items = new List<ProductSearchWithQuantityResponseDto>();
        foreach (var bp in products.Items)
        {
            var canBeMadeCounts =
                (from li in bp.Components
                    let requiredQuantity =
                        li.UsedQuantity
                    let availableQuantity =
                        li.Component.CurrentStock
                    select (int)(availableQuantity / requiredQuantity)).ToList();
            var minCanBeMade = canBeMadeCounts.Any() ? canBeMadeCounts.Min() : 0;
            if (minCanBeMade > 0)
                items.Add(new ProductSearchWithQuantityResponseDto(bp.Id, bp.Name, bp.Identifier, bp.DateCreated,
                    minCanBeMade));
        }

        var response = new PaginatedResult<ProductSearchWithQuantityResponseDto>
        {
            Items = items,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = products.TotalCount
        };
        return Result.Success(response);
    }
}