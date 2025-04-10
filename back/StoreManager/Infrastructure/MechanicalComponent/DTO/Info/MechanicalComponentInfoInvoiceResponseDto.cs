namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Info
{
    public record MechanicalComponentInfoInvoiceResponseDto(
        Guid id,
        DateOnly dateIssued,
        MechanicalComponentInfoProviderResponseDto provider);
}