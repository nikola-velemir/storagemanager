using MediatR;
using StoreManager.Application.Product.DTO;
using StoreManager.Application.Shared;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Product.Command;

public record FindFilteredProductsQuery(string? ProductInfo, string? DateCreated, int PageNumber, int PageSize) : IRequest<PaginatedResult<ProductSearchResponseDto>>;