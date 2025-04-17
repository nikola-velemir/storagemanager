using StoreManager.Infrastructure.Invoice.Export.DTO;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Export.Service;

public interface IExportService
{
    Task Create(CreateExportRequestDto dto);
    Task<PaginatedResult<ExportSearchResponseDto>> FindFiltered(int pageNumber, int pageSize);
}