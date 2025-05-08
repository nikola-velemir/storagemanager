using StoreManager.Application.Invoice.Export.DTO;
using StoreManager.Application.Shared;

namespace StoreManager.Domain.Invoice.Export.Service;

public interface IExportService
{
    Task Create(CreateExportRequestDto dto);
    Task<PaginatedResult<ExportSearchResponseDto>> FindFiltered(string? ExporterId, string? ProductInfo, string? Date,
        int PageNumber, int PageSize);
}