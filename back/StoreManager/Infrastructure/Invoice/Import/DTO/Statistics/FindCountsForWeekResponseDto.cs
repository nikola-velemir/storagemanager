namespace StoreManager.Infrastructure.Invoice.Import.DTO.Statistics
{
    public sealed record FindCountsForWeekResponseDto(List<FindCountForDayResponseDto> Counts);
}
