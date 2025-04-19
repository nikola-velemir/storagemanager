using MediatR;
using StoreManager.Application.Product.DTO;

namespace StoreManager.Application.Product.Command;

public record FindProductInfoQuery(string Id) : IRequest<ProductInfoResponseDto>;