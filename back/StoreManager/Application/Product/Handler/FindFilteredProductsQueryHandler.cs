using MediatR;
using StoreManager.Application.Product.Command;
using StoreManager.Application.Product.DTO;
using StoreManager.Application.Product.Repository;
using StoreManager.Application.Shared;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Product.Handler;

public class FindFilteredProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<FindFilteredProductsQuery, PaginatedResult<ProductSearchResponseDto>>
{
    public async Task<PaginatedResult<ProductSearchResponseDto>> Handle(FindFilteredProductsQuery request,
        CancellationToken cancellationToken)
    {
        DateOnly? date = null;
        if (DateOnly.TryParse(request.DateCreated, out var tempDate))
        {
            date = tempDate;
        }

        var products = await productRepository.FindFiltered(request.ProductInfo, date,
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