namespace StoreManager.Infrastructure.Invoice.DTO.Statistics
{
    public sealed record TotalInvetoryValueResponseDto(double total, List<InventoryValueForDayResponseDto> values);
}
