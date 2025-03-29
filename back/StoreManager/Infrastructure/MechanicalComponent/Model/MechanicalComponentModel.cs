namespace StoreManager.Infrastructure.MechanicalComponent.Model
{
    public class MechanicalComponentModel
    {
        public required Guid Id { get; set; }
        public required string Identifier { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
