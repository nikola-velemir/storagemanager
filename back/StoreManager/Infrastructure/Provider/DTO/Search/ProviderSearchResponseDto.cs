namespace StoreManager.Infrastructure.Provider.DTO.Search
{
    public record class ProviderSearchResponseDto(Guid id, string name, string address, string phoneNumber, List<ProviderInvoiceSearchResponseDto> invoices);
}
