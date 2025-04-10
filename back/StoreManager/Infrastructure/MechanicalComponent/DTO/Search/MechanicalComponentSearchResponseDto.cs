namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Search
{
    public record MechanicalComponentSearchResponseDto(
        Guid id,
        string identifier,
        string name,
        List<MechanicalComponentSearchInvoiceResponseDto> invoices);
}