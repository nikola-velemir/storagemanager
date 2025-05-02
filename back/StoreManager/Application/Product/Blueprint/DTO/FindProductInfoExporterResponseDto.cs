namespace StoreManager.Application.Product.Blueprint.DTO;

public sealed record FindProductInfoExporterResponseDto(Guid Id,
    string Name,
    string Address,
    string PhoneNumber);