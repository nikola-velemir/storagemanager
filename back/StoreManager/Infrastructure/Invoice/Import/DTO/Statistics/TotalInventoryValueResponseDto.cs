namespace StoreManager.Infrastructure.Invoice.Import.DTO.Statistics
{
    public sealed record TotalInventoryValueResponseDto(double Total, List<InventoryValueForDayResponseDto> Values);
}
