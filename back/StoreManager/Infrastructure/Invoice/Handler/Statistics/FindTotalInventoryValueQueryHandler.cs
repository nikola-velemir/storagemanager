using MediatR;
using StoreManager.Infrastructure.Invoice.Command.Statistics;
using StoreManager.Infrastructure.Invoice.DTO.Statistics;
using StoreManager.Infrastructure.Invoice.Repository;

namespace StoreManager.Infrastructure.Invoice.Handler.Statistics
{
    public class FindTotalInventoryValueQueryHandler : IRequestHandler<FindTotalInventoryValueQuery, TotalInventoryValueResponseDto>
    {
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        public FindTotalInventoryValueQueryHandler(IInvoiceItemRepository invoiceItemRepository)
        {
            _invoiceItemRepository = invoiceItemRepository;
        }

        public async Task<TotalInventoryValueResponseDto> Handle(FindTotalInventoryValueQuery request, CancellationToken cancellationToken)
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
            return new TotalInventoryValueResponseDto(total, responses);
        }
    }
}
