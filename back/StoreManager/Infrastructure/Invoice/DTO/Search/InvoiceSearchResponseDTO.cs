namespace StoreManager.Infrastructure.Invoice.DTO.Search
{
    public sealed record class InvoiceSearchResponseDTO(Guid id, DateOnly date, InvoiceSearchProviderDTO provider, List<InvoiceSearchComponentDTO> components);

}
