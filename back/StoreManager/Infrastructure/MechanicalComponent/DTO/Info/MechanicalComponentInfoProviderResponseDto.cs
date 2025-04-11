namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Info
{
    public sealed record MechanicalComponentInfoProviderResponseDto(
        Guid id,
        string name,
        string address,
        string phoneNumber);
}