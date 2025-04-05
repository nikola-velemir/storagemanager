namespace StoreManager.Infrastructure.Invoice.DTO
{
    public sealed record class InvoiceSearchComponentDTO(string name, string identifier, int quantity, double price);
}
