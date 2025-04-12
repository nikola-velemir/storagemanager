using MediatR;
using StoreManager.Infrastructure.Product.DTO;

namespace StoreManager.Infrastructure.Product.Command;

public record CreateProductCommand(string Name, string Description, List<ProductCreateRequestComponentDto> Components) : IRequest;