namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Find
{
    public sealed record MechanicalComponentFindResponseDto(
        Guid id,
        string identifier,
        string name,
        int quantity,
        double price);
}