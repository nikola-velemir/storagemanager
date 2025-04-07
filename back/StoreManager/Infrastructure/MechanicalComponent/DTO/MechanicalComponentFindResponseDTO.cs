namespace StoreManager.Infrastructure.MechanicalComponent.DTO
{
    public record class MechanicalComponentFindResponseDTO(Guid id, string identifier, string name, List<MechanicalComponentInvoiceResponseDTO> invoices);
}
