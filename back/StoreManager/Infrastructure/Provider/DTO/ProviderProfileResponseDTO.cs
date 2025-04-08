namespace StoreManager.Infrastructure.Provider.DTO
{
    public record class ProviderProfileResponseDTO(string name, string address, string phoneNumber,List<ProviderProfileComponentResponseDTO> components, List<ProviderProfileInvoiceResponseDTO> invoices);
}
