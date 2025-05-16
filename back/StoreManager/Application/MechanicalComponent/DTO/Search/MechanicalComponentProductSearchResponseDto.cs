namespace StoreManager.Application.MechanicalComponent.DTO.Search;

public sealed record  MechanicalComponentProductSearchResponseDto(Guid Id,
    string Identifier,
    string Name, int Quantity);