namespace StoreManager.Application.MechanicalComponent.DTO.Info
{
    public sealed record MechanicalComponentInfoInvoiceResponseDto(
        Guid Id,
        DateOnly DateIssued,
        MechanicalComponentInfoProviderResponseDto Provider);
}