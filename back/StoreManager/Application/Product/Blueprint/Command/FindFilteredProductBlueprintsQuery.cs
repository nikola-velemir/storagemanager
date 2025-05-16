using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Product.Blueprint.DTO;
using StoreManager.Application.Shared;

namespace StoreManager.Application.Product.Blueprint.Command;

public record FindFilteredProductBlueprintsQuery(string? ProductInfo, string? DateCreated, int PageNumber, int PageSize) 
    : IRequest<Result<PaginatedResult<ProductSearchResponseDto>>>;