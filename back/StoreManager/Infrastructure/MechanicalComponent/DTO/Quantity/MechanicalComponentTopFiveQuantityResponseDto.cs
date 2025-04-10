namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Quantity
{
    public sealed record MechanicalComponentTopFiveQuantityResponseDto(
        Guid id,
        string name,
        string identifier,
        int quantity);
}