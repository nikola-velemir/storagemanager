namespace StoreManager.Infrastructure.Invoice.DTO
{
    public record class InvoiceFilterRequestDTO(string providerId, string dateIssued, int pageNumber, int pageSize);
}
