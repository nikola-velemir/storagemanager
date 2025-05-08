using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Invoice.Import.Command.Statistics;
using StoreManager.Application.Invoice.Import.DTO.Statistics;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Domain.Utils;
using StoreManager.Infrastructure.Invoice.Import.Repository;

namespace StoreManager.Application.Invoice.Import.Handler.Statistics
{
    public class FindImportCountThisWeekQueryHandler(IImportRepository importRepository)
        : IRequestHandler<FindImportCountThisWeekQuery, Result<FindCountsForWeekResponseDto>>
    {
        public async Task<Result<FindCountsForWeekResponseDto>> Handle(FindImportCountThisWeekQuery request,
            CancellationToken cancellationToken)
        {
            var startOfWeek = DateOnly.FromDateTime(DateTime.UtcNow.StartOfWeek());
            var endOfWeek = startOfWeek.AddDays(7);
            var counts = new List<FindCountForDayResponseDto>();
            for (var date = startOfWeek; date < endOfWeek; date = date.AddDays(1))
            {
                var count = await importRepository.FindCountForTheDateAsync(date);
                counts.Add(new FindCountForDayResponseDto(date.DayOfWeek.ToString(), count));
            }

            return Result.Success(new FindCountsForWeekResponseDto(counts));
        }
    }
}