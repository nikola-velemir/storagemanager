using MediatR;
using StoreManager.Infrastructure.BusinessPartner.Exporter.DTO;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.BusinessPartner.Exporter.Command;

public sealed record FindFilteredQuery(string? ExporterInfo, int PageNumber, int PageSize) : IRequest<PaginatedResult<ExporterSearchResponseDto>>;