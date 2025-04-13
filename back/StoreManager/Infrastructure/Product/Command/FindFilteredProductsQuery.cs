using MediatR;
using StoreManager.Infrastructure.Product.DTO;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Product.Command;

public record FindFilteredProductsQuery(string? ProductInfo, string? DateCreated, int PageNumber, int PageSize) : IRequest<PaginatedResult<ProductSearchResponseDto>>;