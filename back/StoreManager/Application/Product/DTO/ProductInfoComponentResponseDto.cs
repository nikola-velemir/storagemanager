namespace StoreManager.Application.Product.DTO;

public sealed record ProductInfoComponentResponseDto(Guid Id, string Name, string Identifier, int Quantity);