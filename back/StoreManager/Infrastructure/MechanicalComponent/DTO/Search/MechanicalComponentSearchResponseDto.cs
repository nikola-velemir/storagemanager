namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Search
{
    public sealed record MechanicalComponentSearchResponseDto(
        Guid Id,
        string Identifier,
        string Name,
        List<MechanicalComponentSearchInvoiceResponseDto> Invoices);
}