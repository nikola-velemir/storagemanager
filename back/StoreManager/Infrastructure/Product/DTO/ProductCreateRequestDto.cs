namespace StoreManager.Infrastructure.Product.DTO;

public sealed record ProductCreateRequestDto(string Name, string Description, List<ProductCreateRequestComponentDto> Components);