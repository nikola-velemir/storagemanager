namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Info
{
    public record MechanicalComponentInfoProviderResponseDto(
        Guid id,
        string name,
        string address,
        string phoneNumber);
}