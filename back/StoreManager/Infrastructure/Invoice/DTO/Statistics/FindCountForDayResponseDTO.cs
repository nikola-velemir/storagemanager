namespace StoreManager.Infrastructure.Invoice.DTO.Statistics
{
    public sealed record class FindCountForDayResponseDTO(string dayOfTheWeek, int count);
}
