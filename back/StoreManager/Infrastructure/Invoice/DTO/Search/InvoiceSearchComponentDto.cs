namespace StoreManager.Infrastructure.Invoice.DTO.Search
{
    public sealed record class InvoiceSearchComponentDto(Guid id,string name, string identifier, int quantity, double price);
}
