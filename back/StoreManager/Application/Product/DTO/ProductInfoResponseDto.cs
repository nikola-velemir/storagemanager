namespace StoreManager.Application.Product.DTO;

public sealed record ProductInfoResponseDto(
    string Name,
    string Description,
    string Identifier,
    DateOnly DateCreated,
    List<ProductInfoComponentResponseDto> Components,
    List<ProductInfoExportResponseDto> Exports);