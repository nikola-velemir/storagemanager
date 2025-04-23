
using MediatR;
using StoreManager.Application.BusinessPartner.Exporter.Repository;
using StoreManager.Application.Invoice.Export.Command;
using StoreManager.Application.Invoice.Export.Repository;
using StoreManager.Application.Invoice.Import.DTO.Statistics;
using StoreManager.Domain.Utils;

namespace StoreManager.Application.Invoice.Export.Handler;

public class FindExportCountThisWeekQueryHandler(IExportRepository exportRepository) : IRequestHandler<FindExportCountThisWeekQuery, FindCountsForWeekResponseDto>
{
    public async Task<FindCountsForWeekResponseDto> Handle(FindExportCountThisWeekQuery request, CancellationToken cancellationToken)
    {
        var startOfWeek = DateOnly.FromDateTime(DateTime.UtcNow.StartOfWeek());
        var endOfWeek = startOfWeek.AddDays(7);
        var counts = new List<FindCountForDayResponseDto>();
        for (var date = startOfWeek; date < endOfWeek; date = date.AddDays(1))
        {
            var count = await exportRepository.FindCountForDateAsync(date);
            counts.Add(new FindCountForDayResponseDto(date.DayOfWeek.ToString(),count));
        }

        return new FindCountsForWeekResponseDto(counts);
    }
}