namespace StoreManager.Application.Product.DTO;

public sealed record ProductSearchResponseDto(Guid Id, string Name, string Identifier, DateOnly DateCreated);