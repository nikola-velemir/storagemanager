namespace StoreManager.Infrastructure.Product.DTO;

public sealed record ProductInfoComponentResponseDto(Guid Id, string Name, string Identifier, int Quantity);