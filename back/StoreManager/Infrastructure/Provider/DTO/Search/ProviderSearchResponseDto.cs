namespace StoreManager.Infrastructure.Provider.DTO.Search
{
    public sealed record ProviderSearchResponseDto(
        Guid id,
        string name,
        string address,
        string phoneNumber,
        List<ProviderInvoiceSearchResponseDto> invoices);
}