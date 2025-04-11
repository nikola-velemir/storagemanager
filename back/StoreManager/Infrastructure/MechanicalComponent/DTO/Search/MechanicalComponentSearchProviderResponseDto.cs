namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Search
{
    public sealed record MechanicalComponentSearchProviderResponseDto(
        Guid id,
        string name,
        string address,
        string phoneNumber);
}