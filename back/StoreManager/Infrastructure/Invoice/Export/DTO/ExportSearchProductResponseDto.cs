namespace StoreManager.Infrastructure.Invoice.Export.DTO;

public sealed record ExportSearchProductResponseDto(string Name, string Identifier, int Quantity, double Price);