using MediatR;
using StoreManager.Application.Invoice.Export.Command;
using StoreManager.Application.Invoice.Export.Repository;
using StoreManager.Application.Invoice.Import.DTO.Statistics;

namespace StoreManager.Application.Invoice.Export.Handler;

public class CountExportsThisWeekQueryHandler(IExportRepository exportRepository)
    : IRequestHandler<CountExportsThisWeekQuery, ThisWeekInvoiceCountResponseDto>
{
    public async Task<ThisWeekInvoiceCountResponseDto> Handle(CountExportsThisWeekQuery request,
        CancellationToken cancellationToken)
    {
        return new ThisWeekInvoiceCountResponseDto(await exportRepository.CountExportsThisWeekAsync());
    }
}