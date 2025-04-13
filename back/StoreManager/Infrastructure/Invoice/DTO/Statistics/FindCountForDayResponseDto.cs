namespace StoreManager.Infrastructure.Invoice.DTO.Statistics
{
    public sealed record FindCountForDayResponseDto(string DayOfTheWeek, int Count);
}
