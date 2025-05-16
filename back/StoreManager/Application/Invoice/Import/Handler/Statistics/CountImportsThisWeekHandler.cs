using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Invoice.Import.Command.Statistics;
using StoreManager.Application.Invoice.Import.DTO.Statistics;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Infrastructure.Invoice.Import.Repository;

namespace StoreManager.Application.Invoice.Import.Handler.Statistics
{
    public class CountImportsThisWeekHandler(IImportRepository importRepository)
        : IRequestHandler<CountImportsThisWeekQuery, Result<ThisWeekInvoiceCountResponseDto>>
    {
        public async Task<Result<ThisWeekInvoiceCountResponseDto>> Handle(CountImportsThisWeekQuery request,
            CancellationToken cancellationToken)
        {
            return Result.Success(new ThisWeekInvoiceCountResponseDto(await importRepository.CountImportsThisWeek()));
        }
    }
}