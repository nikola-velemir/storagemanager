using MediatR;
using StoreManager.Application.Product.Blueprint.DTO;

namespace StoreManager.Application.Product.Blueprint.Command;

public record FindProductBlueprintsByPartnerQuery(string Id) : IRequest<List<ProductSearchResponseDto>>;