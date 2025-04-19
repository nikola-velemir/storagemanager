using MediatR;
using StoreManager.Application.BusinessPartner.Exporter.DTO;
using StoreManager.Application.Shared;

namespace StoreManager.Application.BusinessPartner.Exporter.Command;

public sealed record FindFilteredQuery(string? ExporterInfo, int PageNumber, int PageSize) : IRequest<PaginatedResult<ExporterSearchResponseDto>>;