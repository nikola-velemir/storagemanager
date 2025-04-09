namespace StoreManager.Infrastructure.Invoice.DTO.Search
{
    public record class InvoiceFilterRequestDTO(string providerId, string dateIssued, int pageNumber, int pageSize);
}
