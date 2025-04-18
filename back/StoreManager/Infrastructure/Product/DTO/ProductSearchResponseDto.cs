namespace StoreManager.Infrastructure.Product.DTO;

public sealed record ProductSearchResponseDto(Guid Id, string Name, string Identifier, DateOnly DateCreated);