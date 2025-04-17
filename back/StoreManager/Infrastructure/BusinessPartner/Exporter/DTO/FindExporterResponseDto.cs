namespace StoreManager.Infrastructure.BusinessPartner.Exporter.DTO;

public sealed record FindExporterResponseDto(
    Guid Id,
    string Name,
    string Address,
    string PhoneNumber);