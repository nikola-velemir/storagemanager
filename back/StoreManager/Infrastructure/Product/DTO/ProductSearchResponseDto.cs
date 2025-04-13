namespace StoreManager.Infrastructure.Product.DTO;

public sealed record ProductSearchResponseDto(Guid id, string Name, string Identifier, DateOnly DateCreated);