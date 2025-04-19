namespace StoreManager.Application.Invoice.Export.DTO;

public sealed record CreateExportRequestProductDto(string id, int quantity, double price);