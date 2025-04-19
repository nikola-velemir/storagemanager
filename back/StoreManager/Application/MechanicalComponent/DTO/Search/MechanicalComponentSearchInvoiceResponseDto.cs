namespace StoreManager.Application.MechanicalComponent.DTO.Search
{
    public sealed record MechanicalComponentSearchInvoiceResponseDto(
        Guid Id,
        DateOnly DateIssued,
        MechanicalComponentSearchProviderResponseDto Provider);
}