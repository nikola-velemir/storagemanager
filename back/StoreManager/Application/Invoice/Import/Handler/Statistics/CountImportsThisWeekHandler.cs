using MediatR;
using StoreManager.Application.Invoice.Import.Command.Statistics;
using StoreManager.Application.Invoice.Import.DTO.Statistics;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Infrastructure.Invoice.Import.Repository;

namespace StoreManager.Application.Invoice.Import.Handler.Statistics
{
    public class CountImportsThisWeekHandler(IImportRepository importRepository)
        : IRequestHandler<CountImportsThisWeekQuery, ThisWeekInvoiceCountResponseDto>
    {
        public async Task<ThisWeekInvoiceCountResponseDto> Handle(CountImportsThisWeekQuery request, CancellationToken cancellationToken)
        {
            return new ThisWeekInvoiceCountResponseDto(await importRepository.CountImportsThisWeek());
        }
    }
}
