namespace StoreManager.Infrastructure.Invoice.DTO
{
    public sealed record class FindCountForDayResponseDTO(string dayOfTheWeek, int count);
}
