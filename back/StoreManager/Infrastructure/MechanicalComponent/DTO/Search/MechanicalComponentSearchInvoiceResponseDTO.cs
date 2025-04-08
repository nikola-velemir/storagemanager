namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Search
{
    public record class MechanicalComponentSearchInvoiceResponseDTO(Guid id, DateOnly dateIssued, MechanicalComponentSearchProviderResponseDTO provider);
}
