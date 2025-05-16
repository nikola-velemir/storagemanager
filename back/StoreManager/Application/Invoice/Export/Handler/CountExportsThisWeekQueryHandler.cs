using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Invoice.Export.Command;
using StoreManager.Application.Invoice.Export.Repository;
using StoreManager.Application.Invoice.Import.DTO.Statistics;

namespace StoreManager.Application.Invoice.Export.Handler;

public class CountExportsThisWeekQueryHandler(IExportRepository exportRepository)
    : IRequestHandler<CountExportsThisWeekQuery, Result<ThisWeekInvoiceCountResponseDto>>
{
    public async Task<Result<ThisWeekInvoiceCountResponseDto>> Handle(CountExportsThisWeekQuery request,
        CancellationToken cancellationToken)
    {
        return Result.Success(new ThisWeekInvoiceCountResponseDto(await exportRepository.CountExportsThisWeekAsync()));
    }
}