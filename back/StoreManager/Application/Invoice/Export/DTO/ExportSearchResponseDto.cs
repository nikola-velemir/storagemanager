namespace StoreManager.Application.Invoice.Export.DTO;

public sealed record ExportSearchResponseDto(
    Guid Id,
    DateOnly Date,
    string ExporterName,
    List<ExportSearchProductResponseDto> Products);