namespace StoreManager.Infrastructure.Invoice.DTO.Search
{
    public record class InvoiceFilterRequestDto(string providerId, string dateIssued, int pageNumber, int pageSize);
}
