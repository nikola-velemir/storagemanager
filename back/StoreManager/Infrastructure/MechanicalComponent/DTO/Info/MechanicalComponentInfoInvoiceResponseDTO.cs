namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Info
{
    public record class MechanicalComponentInfoInvoiceResponseDTO(Guid id, DateOnly dateIssued, MechanicalComponentInfoProviderResponseDTO provider);
}
