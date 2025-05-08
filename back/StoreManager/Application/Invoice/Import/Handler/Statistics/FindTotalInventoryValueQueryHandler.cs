using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Invoice.Import.Command.Statistics;
using StoreManager.Application.Invoice.Import.DTO.Statistics;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Infrastructure.Invoice.Import.Repository;

namespace StoreManager.Application.Invoice.Import.Handler.Statistics
{
    public class FindTotalInventoryValueQueryHandler(IImportRepository importRepository)
        : IRequestHandler<FindTotalInventoryValueQuery, Result<TotalInventoryValueResponseDto>>
    {
        public async Task<Result<TotalInventoryValueResponseDto>> Handle(FindTotalInventoryValueQuery request,
            CancellationToken cancellationToken)
        {
            var total = await importRepository.FindTotalPrice();
            var endDate = DateOnly.FromDateTime(DateTime.UtcNow);
            var startDate = endDate.AddDays(-6);
            var responses = new List<InventoryValueForDayResponseDto>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var sum = await importRepository.FindSumForDate(date);
                responses.Add(new InventoryValueForDayResponseDto(date.DayOfWeek.ToString(), sum));
            }

            return Result.Success(new TotalInventoryValueResponseDto(total, responses));
        }
    }
}