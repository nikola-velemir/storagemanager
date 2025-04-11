namespace StoreManager.Infrastructure.Invoice.DTO.Statistics
{
    public sealed record TotalInventoryValueResponseDto(double total, List<InventoryValueForDayResponseDto> values);
}
