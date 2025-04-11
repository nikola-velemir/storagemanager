namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Info
{
    public sealed record MechanicalComponentInfoInvoiceResponseDto(
        Guid id,
        DateOnly dateIssued,
        MechanicalComponentInfoProviderResponseDto provider);
}