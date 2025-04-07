namespace StoreManager.Infrastructure.Provider.DTO
{
    public record class ProviderSearchResponseDTO(Guid id, string name, string address, string phoneNumber, List<ProviderInvoiceSearchResponseDTO> invoices);
}
