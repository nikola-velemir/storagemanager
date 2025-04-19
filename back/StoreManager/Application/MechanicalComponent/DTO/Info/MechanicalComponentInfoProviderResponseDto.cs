namespace StoreManager.Application.MechanicalComponent.DTO.Info
{
    public sealed record MechanicalComponentInfoProviderResponseDto(
        Guid Id,
        string Name,
        string Address,
        string PhoneNumber);
}