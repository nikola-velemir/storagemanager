using MediatR;
using StoreManager.Infrastructure.Invoice.Export.DTO;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Export.Command;

public record ExportSearchQuery(string? ExporterId, string? ProductInfo, string? Date, int PageNumber, int PageSize) : IRequest<PaginatedResult<ExportSearchResponseDto>>;