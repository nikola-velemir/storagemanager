namespace StoreManager.Infrastructure.Invoice.DTO
{
    public record class InvoiceFilterDTO(string providerId, string dateIssued, int pageNumber, int pageSize);
}
