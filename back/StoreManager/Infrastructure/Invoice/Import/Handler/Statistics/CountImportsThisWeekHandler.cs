using MediatR;
using StoreManager.Infrastructure.Invoice.Import.Command.Statistics;
using StoreManager.Infrastructure.Invoice.Import.DTO.Statistics;
using StoreManager.Infrastructure.Invoice.Import.Repository;

namespace StoreManager.Infrastructure.Invoice.Import.Handler.Statistics
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
