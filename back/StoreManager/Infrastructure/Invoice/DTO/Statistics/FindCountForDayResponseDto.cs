namespace StoreManager.Infrastructure.Invoice.DTO.Statistics
{
    public sealed record class FindCountForDayResponseDto(string dayOfTheWeek, int count);
}
