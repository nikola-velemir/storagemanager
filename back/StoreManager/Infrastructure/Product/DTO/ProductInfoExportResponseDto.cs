namespace StoreManager.Infrastructure.Product.DTO;

public sealed record ProductInfoExportResponseDto(Guid Id, DateOnly Date, FindProductInfoExporterResponseDto Exporter);