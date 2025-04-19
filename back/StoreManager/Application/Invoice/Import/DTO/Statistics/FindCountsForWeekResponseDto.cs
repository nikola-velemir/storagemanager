namespace StoreManager.Application.Invoice.Import.DTO.Statistics
{
    public sealed record FindCountsForWeekResponseDto(List<FindCountForDayResponseDto> Counts);
}
