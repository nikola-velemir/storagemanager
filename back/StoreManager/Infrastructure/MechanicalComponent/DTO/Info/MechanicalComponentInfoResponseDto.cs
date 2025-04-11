namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Info
{
    public sealed record MechanicalComponentInfoResponseDto(
        string name,
        string identifier,
        int quantity,
        List<MechanicalComponentInfoInvoiceResponseDto> invoices);
}