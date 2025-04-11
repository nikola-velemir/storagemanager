namespace StoreManager.Infrastructure.Provider.DTO
{
    public sealed record ProviderCreateRequestDto(string name, string address, string phoneNumber);
}