namespace StoreManager.Infrastructure.MechanicalComponent.DTO
{
    public record class MechanicalComponentInvoiceResponseDTO(Guid id, DateOnly dateIssued, MechanicalComponentProviderResponseDTO provider);
}
