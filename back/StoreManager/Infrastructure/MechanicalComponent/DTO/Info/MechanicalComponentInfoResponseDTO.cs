namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Info
{
    public record class MechanicalComponentInfoResponseDTO(string name, string identifier, int quantity, List<MechanicalComponentInfoInvoiceResponseDTO> invoices);
}
