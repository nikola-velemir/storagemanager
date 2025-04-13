namespace StoreManager.Infrastructure.Invoice.DTO.Statistics
{
    public sealed record TotalInventoryValueResponseDto(double Total, List<InventoryValueForDayResponseDto> Values);
}
