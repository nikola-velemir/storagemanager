namespace StoreManager.Infrastructure.Invoice.DTO.Statistics
{
    public sealed record TotalInvetoryValueResponseDTO(double total, List<InventoryValueForDayResponseDTO> values);
}
