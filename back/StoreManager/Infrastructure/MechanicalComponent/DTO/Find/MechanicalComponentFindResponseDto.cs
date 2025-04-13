namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Find
{
    public sealed record MechanicalComponentFindResponseDto(
        Guid Id,
        string Identifier,
        string Name,
        int Quantity,
        double Price);
}