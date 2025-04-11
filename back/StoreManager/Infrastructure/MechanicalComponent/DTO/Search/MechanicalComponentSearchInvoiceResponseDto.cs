namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Search
{
    public sealed record MechanicalComponentSearchInvoiceResponseDto(
        Guid id,
        DateOnly dateIssued,
        MechanicalComponentSearchProviderResponseDto provider);
}