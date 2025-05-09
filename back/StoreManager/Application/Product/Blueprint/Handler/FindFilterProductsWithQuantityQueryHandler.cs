using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Product.Blueprint.Command;
using StoreManager.Application.Product.Blueprint.DTO;
using StoreManager.Application.Product.Blueprint.Repository;
using StoreManager.Application.Shared;
using StoreManager.Domain.Product.Blueprint.Specification;

namespace StoreManager.Application.Product.Blueprint.Handler;

public class FindFilterProductsWithQuantityQueryHandler(IProductBlueprintRepository productBlueprintRepository) : 
    IRequestHandler<FindFilteredProductBlueprintWithQuantityQuery, Result<PaginatedResult<ProductSearchWithQuantityResponseDto>>>
{
    public async Task<Result<PaginatedResult<ProductSearchWithQuantityResponseDto>>> Handle(FindFilteredProductBlueprintWithQuantityQuery request, CancellationToken cancellationToken)
    {
        DateOnly? date = null;
        if (DateOnly.TryParse(request.DateCreated, out var tempDate))
        {
            date = tempDate;
        }

        var products = await productBlueprintRepository.FindFilteredAsync(request.ProductInfo, date,
            request.PageNumber, request.PageSize, new ProductBlueprintsWithBatchesSpecification());
        var items = products.Items.Select(p=> new ProductSearchWithQuantityResponseDto(
            p.Id,
            p.Name,
            p.Identifier,
            p.DateCreated,
            p.Batches.Sum(pb=>pb.Quantity)
            )).ToList();
        
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