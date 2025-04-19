using MediatR;
using StoreManager.Application.Invoice.Export.DTO;
using StoreManager.Application.Shared;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Invoice.Export.Command;

public record ExportSearchQuery(string? ExporterId, string? ProductInfo, string? Date, int PageNumber, int PageSize) : IRequest<PaginatedResult<ExportSearchResponseDto>>;