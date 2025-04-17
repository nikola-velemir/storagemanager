namespace StoreManager.Infrastructure.Invoice.Export.DTO;

public sealed record CreateExportRequestProductDto(string id, int quantity, double price);