using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Invoice.Import.DTO.Statistics;

namespace StoreManager.Application.Invoice.Export.Command;

public record FindExportCountThisWeekQuery() : IRequest<Result<FindCountsForWeekResponseDto>>;