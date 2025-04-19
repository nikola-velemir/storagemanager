namespace StoreManager.Application.MechanicalComponent.DTO.Info
{
    public sealed record MechanicalComponentInfoResponseDto(
        string Name,
        string Identifier,
        int Quantity,
        List<MechanicalComponentInfoInvoiceResponseDto> Invoices);
}