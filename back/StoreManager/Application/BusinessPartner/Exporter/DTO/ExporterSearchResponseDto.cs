namespace StoreManager.Application.BusinessPartner.Exporter.DTO;

public sealed record ExporterSearchResponseDto(
    Guid Id,
    string Name,
    string Address,
    string PhoneNumber,
    List<ExporterSearchExportResponse> Exports);