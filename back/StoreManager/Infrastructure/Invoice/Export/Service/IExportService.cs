using StoreManager.Infrastructure.Invoice.Export.DTO;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Export.Service;

public interface IExportService
{
    Task Create(CreateExportRequestDto dto);
    Task<PaginatedResult<ExportSearchResponseDto>> FindFiltered(string? ExporterId, string? ProductInfo, string? Date,
        int PageNumber, int PageSize);
}