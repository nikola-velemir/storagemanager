using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Product.Blueprint.DTO;

namespace StoreManager.Application.Product.Blueprint.Command;

public record CreateProductBlueprintCommand(string Name, string Identifier, string Description, List<ProductCreateRequestComponentDto> Components) : IRequest<Result>;