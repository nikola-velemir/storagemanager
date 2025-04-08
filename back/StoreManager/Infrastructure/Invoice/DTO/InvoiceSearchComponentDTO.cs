namespace StoreManager.Infrastructure.Invoice.DTO
{
    public sealed record class InvoiceSearchComponentDTO(Guid id,string name, string identifier, int quantity, double price);
}
