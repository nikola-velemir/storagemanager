namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Find
{
    public record MechanicalComponentFindResponseDto(
        Guid id,
        string identifier,
        string name,
        int quantity,
        double price);
}