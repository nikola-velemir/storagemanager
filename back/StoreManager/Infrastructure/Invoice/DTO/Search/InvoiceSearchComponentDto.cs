namespace StoreManager.Infrastructure.Invoice.DTO.Search
{
    public sealed record  InvoiceSearchComponentDto(Guid id,string name, string identifier, int quantity, double price);
}
