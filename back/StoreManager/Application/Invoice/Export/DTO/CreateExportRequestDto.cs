namespace StoreManager.Application.Invoice.Export.DTO;

public sealed record CreateExportRequestDto(string providerId, List<CreateExportRequestProductDto> products);