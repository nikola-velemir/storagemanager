namespace StoreManager.Application.Invoice.Export.DTO;

public sealed record ExportSearchProductResponseDto(Guid Id,string Name, string Identifier, int Quantity, double Price);