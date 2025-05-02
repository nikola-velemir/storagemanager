namespace StoreManager.Application.Product.Blueprint.DTO;

public sealed record ProductInfoExportResponseDto(Guid Id, DateOnly Date, FindProductInfoExporterResponseDto Exporter);