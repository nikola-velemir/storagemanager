namespace StoreManager.Infrastructure.Invoice.DTO.Statistics
{
    public sealed record FindCountForDayResponseDto(string dayOfTheWeek, int count);
}
