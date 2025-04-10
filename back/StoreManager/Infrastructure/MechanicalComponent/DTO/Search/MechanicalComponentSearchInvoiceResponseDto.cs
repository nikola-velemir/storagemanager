namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Search
{
    public record MechanicalComponentSearchInvoiceResponseDto(
        Guid id,
        DateOnly dateIssued,
        MechanicalComponentSearchProviderResponseDto provider);
}