using MediatR;
using StoreManager.Infrastructure.Invoice.Command.Query;
using StoreManager.Infrastructure.Invoice.DTO;
using StoreManager.Infrastructure.Invoice.Repository;

namespace StoreManager.Infrastructure.Invoice.Handler
{
    public class CountInvoicesThisWeekHandler : IRequestHandler<CountInvoicesThisWeekQuery, ThisWeekInvoiceCountResponseDTO>
    {
        private IInvoiceRepository _invoiceRepository;
        public CountInvoicesThisWeekHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<ThisWeekInvoiceCountResponseDTO> Handle(CountInvoicesThisWeekQuery request, CancellationToken cancellationToken)
        {
            return new ThisWeekInvoiceCountResponseDTO(await _invoiceRepository.CountInvoicesThisWeek());
        }
    }
}
