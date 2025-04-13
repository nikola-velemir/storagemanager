namespace StoreManager.Infrastructure.Invoice.DTO.Statistics
{
    public sealed record FindCountsForWeekResponseDto(List<FindCountForDayResponseDto> Counts);
}
