namespace StoreManager.Infrastructure.Product.DTO;

public sealed record FindProductInfoExporterResponseDto(Guid Id,
    string Name,
    string Address,
    string PhoneNumber);