namespace StoreManager.Application.MechanicalComponent.DTO.Search
{
    public sealed record MechanicalComponentSearchProviderResponseDto(
        Guid Id,
        string Name,
        string Address,
        string PhoneNumber);
}