namespace StoreManager.Infrastructure.Provider.Model
{
    public class ProviderModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Adress { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
