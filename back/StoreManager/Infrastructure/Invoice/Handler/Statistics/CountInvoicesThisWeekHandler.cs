using MediatR;
using StoreManager.Infrastructure.Invoice.Command.Statistics;
using StoreManager.Infrastructure.Invoice.DTO.Statistics;
using StoreManager.Infrastructure.Invoice.Repository;

namespace StoreManager.Infrastructure.Invoice.Handler.Statistics
{
    public class CountInvoicesThisWeekHandler : IRequestHandler<CountInvoicesThisWeekQuery, ThisWeekInvoiceCountResponseDto>
    {
        private IInvoiceRepository _invoiceRepository;
        public CountInvoicesThisWeekHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<ThisWeekInvoiceCountResponseDto> Handle(CountInvoicesThisWeekQuery request, CancellationToken cancellationToken)
        {
            return new ThisWeekInvoiceCountResponseDto(await _invoiceRepository.CountInvoicesThisWeek());
        }
    }
}
