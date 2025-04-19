namespace StoreManager.Application.Product.DTO;

public sealed record ProductCreateRequestDto(string Name, string Identifier, string Description, List<ProductCreateRequestComponentDto> Components);