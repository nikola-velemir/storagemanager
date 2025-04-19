namespace StoreManager.Application.MechanicalComponent.DTO.Quantity
{
    public sealed record MechanicalComponentTopFiveQuantityResponseDto(
        Guid Id,
        string Name,
        string Identifier,
        int Quantity);
}