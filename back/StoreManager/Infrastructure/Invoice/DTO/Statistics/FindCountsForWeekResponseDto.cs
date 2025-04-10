namespace StoreManager.Infrastructure.Invoice.DTO.Statistics
{
    public sealed record class FindCountsForWeekResponseDto(List<FindCountForDayResponseDto> counts);
}
