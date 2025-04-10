using MediatR;
using StoreManager.Infrastructure.Invoice.Command.Statistics;
using StoreManager.Infrastructure.Invoice.DTO.Statistics;
using StoreManager.Infrastructure.Invoice.Repository;

namespace StoreManager.Infrastructure.Invoice.Handler.Statistics
{
    public class FindTotalInventoryValueQueryHandler : IRequestHandler<FindTotalInventoryValueQuery, TotalInvetoryValueResponseDto>
    {
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        public FindTotalInventoryValueQueryHandler(IInvoiceItemRepository invoiceItemRepository)
        {
            _invoiceItemRepository = invoiceItemRepository;
        }

        public async Task<TotalInvetoryValueResponseDto> Handle(FindTotalInventoryValueQuery request, CancellationToken cancellationToken)
        {
            var total = await _invoiceItemRepository.FindTotalPrice();
            var endDate = DateOnly.FromDateTime(DateTime.UtcNow);
            var startDate = endDate.AddDays(-6);
            var responses = new List<InventoryValueForDayResponseDto>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var sum = await _invoiceItemRepository.FindSumForDate(date);
                responses.Add(new InventoryValueForDayResponseDto(date.DayOfWeek.ToString(), sum));
            }
            return new TotalInvetoryValueResponseDto(total, responses);
        }
    }
}
