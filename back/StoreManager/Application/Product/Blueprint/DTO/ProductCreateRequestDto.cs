namespace StoreManager.Application.Product.Blueprint.DTO;

public sealed record ProductCreateRequestDto(string Name, string Identifier, string Description, List<ProductCreateRequestComponentDto> Components);