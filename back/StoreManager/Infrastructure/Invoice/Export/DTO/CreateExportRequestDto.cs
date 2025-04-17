using DocumentFormat.OpenXml.Office2010.Excel;

namespace StoreManager.Infrastructure.Invoice.Export.DTO;

public sealed record CreateExportRequestDto(string providerId, List<CreateExportRequestProductDto> products);