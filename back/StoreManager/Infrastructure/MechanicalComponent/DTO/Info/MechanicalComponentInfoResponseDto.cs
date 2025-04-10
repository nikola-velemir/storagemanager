namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Info
{
    public record MechanicalComponentInfoResponseDto(
        string name,
        string identifier,
        int quantity,
        List<MechanicalComponentInfoInvoiceResponseDto> invoices);
}