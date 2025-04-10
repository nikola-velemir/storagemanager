namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Search
{
    public record MechanicalComponentSearchProviderResponseDto(
        Guid id,
        string name,
        string address,
        string phoneNumber);
}