using MediatR;
using StoreManager.Infrastructure.Invoice.Command.Statistics;
using StoreManager.Infrastructure.Invoice.DTO.Statistics;
using StoreManager.Infrastructure.Invoice.Repository;

namespace StoreManager.Infrastructure.Invoice.Handler.Statistics
{
    public class CountInvoicesThisWeekHandler(IInvoiceRepository invoiceRepository)
        : IRequestHandler<CountInvoicesThisWeekQuery, ThisWeekInvoiceCountResponseDto>
    {
        public async Task<ThisWeekInvoiceCountResponseDto> Handle(CountInvoicesThisWeekQuery request, CancellationToken cancellationToken)
        {
            return new ThisWeekInvoiceCountResponseDto(await invoiceRepository.CountInvoicesThisWeek());
        }
    }
}
