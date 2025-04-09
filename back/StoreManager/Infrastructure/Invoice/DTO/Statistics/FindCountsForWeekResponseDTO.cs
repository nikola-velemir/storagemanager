namespace StoreManager.Infrastructure.Invoice.DTO.Statistics
{
    public sealed record class FindCountsForWeekResponseDTO(List<FindCountForDayResponseDTO> counts);
}
