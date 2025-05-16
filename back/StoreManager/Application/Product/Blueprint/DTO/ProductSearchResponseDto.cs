namespace StoreManager.Application.Product.Blueprint.DTO;

public sealed record ProductSearchResponseDto(Guid Id, string Name, string Identifier, DateOnly DateCreated);