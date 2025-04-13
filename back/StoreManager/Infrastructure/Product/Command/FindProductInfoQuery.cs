using MediatR;
using StoreManager.Infrastructure.Product.DTO;

namespace StoreManager.Infrastructure.Product.Command;

public record FindProductInfoQuery(string Id) : IRequest<ProductInfoResponseDto>;