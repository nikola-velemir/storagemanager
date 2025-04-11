namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Search
{
    public sealed record MechanicalComponentSearchResponseDto(
        Guid id,
        string identifier,
        string name,
        List<MechanicalComponentSearchInvoiceResponseDto> invoices);
}