namespace StoreManager.Infrastructure.Invoice.DTO
{
    public sealed record class FindCountsForWeekResponseDTO(List<FindCountForDayResponseDTO> counts);
}
