using MediatR;
using StoreManager.Application.Invoice.Import.Command.Statistics;
using StoreManager.Application.Invoice.Import.DTO.Statistics;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Infrastructure.Invoice.Import.Repository;

namespace StoreManager.Application.Invoice.Import.Handler.Statistics
{
    public class CountImportsThisWeekHandler(IImportRepository importRepository)
        : IRequestHandler<CountInvoicesThisWeekQuery, ThisWeekInvoiceCountResponseDto>
    {
        public async Task<ThisWeekInvoiceCountResponseDto> Handle(CountInvoicesThisWeekQuery request, CancellationToken cancellationToken)
        {
            return new ThisWeekInvoiceCountResponseDto(await importRepository.CountImportsThisWeek());
        }
    }
}
