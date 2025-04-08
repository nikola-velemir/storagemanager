namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Find
{
    public record class MechanicalComponentFindResponseDTO(Guid id, string identifier, string name, int quantity, double price);
}
