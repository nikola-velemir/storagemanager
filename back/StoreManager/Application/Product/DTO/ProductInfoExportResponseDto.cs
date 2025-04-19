namespace StoreManager.Application.Product.DTO;

public sealed record ProductInfoExportResponseDto(Guid Id, DateOnly Date, FindProductInfoExporterResponseDto Exporter);