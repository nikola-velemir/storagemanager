namespace StoreManager.Application.Product.Blueprint.DTO;

public sealed record ProductSearchWithQuantityResponseDto(Guid Id,
    string Name,
    string Identifier, 
    DateOnly DateCreated,
    int Quantity);