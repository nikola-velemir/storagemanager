using MediatR;
using StoreManager.Infrastructure.Invoice.Command.Statistics;
using StoreManager.Infrastructure.Invoice.DTO.Statistics;
using StoreManager.Infrastructure.Invoice.Repository;

namespace StoreManager.Infrastructure.Invoice.Handler.Statistics
{
    public class FindCountsThisWeekQueryHandler : IRequestHandler<FindCountsThisWeekQuery, FindCountsForWeekResponseDto>
    {
        private IInvoiceRepository _invoiceRepository;
        public FindCountsThisWeekQueryHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<FindCountsForWeekResponseDto> Handle(FindCountsThisWeekQuery request, CancellationToken cancellationToken)
        {
            var startOfWeek = DateOnly.FromDateTime(DateTime.Now.StartOfWeek());
            var endOfWeek = startOfWeek.AddDays(7);
            var counts = new List<FindCountForDayResponseDto>();
            for (var date = startOfWeek; date < endOfWeek; date = date.AddDays(1))
            {
                int count = await _invoiceRepository.FindCountForTheDate(date);
                counts.Add(new FindCountForDayResponseDto(date.DayOfWeek.ToString(), count));
            }
            return new FindCountsForWeekResponseDto(counts);
        }
    }
}
