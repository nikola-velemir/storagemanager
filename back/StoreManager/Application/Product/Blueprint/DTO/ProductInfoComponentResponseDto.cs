namespace StoreManager.Application.Product.Blueprint.DTO;

public sealed record ProductInfoComponentResponseDto(Guid Id, string Name, string Identifier, int Quantity);