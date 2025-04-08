namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Search
{
    public record class MechanicalComponentSearchResponseDTO(Guid id, string identifier, string name, List<MechanicalComponentSearchInvoiceResponseDTO> invoices);
}
