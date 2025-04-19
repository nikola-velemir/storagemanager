using MediatR;
using StoreManager.Application.Product.DTO;

namespace StoreManager.Application.Product.Command;

public record CreateProductCommand(string Name, string Identifier, string Description, List<ProductCreateRequestComponentDto> Components) : IRequest;