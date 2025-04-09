namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Quantity
{
    public sealed record class MechanicalComponentTopFiveQuantityResponseDTO(Guid id, string name, string identifier, int quantity);
}
