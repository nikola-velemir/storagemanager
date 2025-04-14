using MediatR;
using StoreManager.Infrastructure.Invoice.Import.Command.Statistics;
using StoreManager.Infrastructure.Invoice.Import.DTO.Statistics;
using StoreManager.Infrastructure.Invoice.Import.Repository;
using StoreManager.Infrastructure.Utils;

namespace StoreManager.Infrastructure.Invoice.Import.Handler.Statistics
{
    public class FindImportCountThisWeekQueryHandler(IImportRepository importRepository)
        : IRequestHandler<FindImportCountThisWeekQuery, FindCountsForWeekResponseDto>
    {
        public async Task<FindCountsForWeekResponseDto> Handle(FindImportCountThisWeekQuery request, CancellationToken cancellationToken)
        {
            var startOfWeek = DateOnly.FromDateTime(DateTime.UtcNow.StartOfWeek());
            var endOfWeek = startOfWeek.AddDays(7);
            var counts = new List<FindCountForDayResponseDto>();
            for (var date = startOfWeek; date < endOfWeek; date = date.AddDays(1))
            {
                var count = await importRepository.FindCountForTheDate(date);
                counts.Add(new FindCountForDayResponseDto(date.DayOfWeek.ToString(), count));
            }
            return new FindCountsForWeekResponseDto(counts);
        }
    }
}
