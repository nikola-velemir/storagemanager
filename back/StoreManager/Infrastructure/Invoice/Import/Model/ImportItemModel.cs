using StoreManager.Infrastructure.MechanicalComponent.Model;

namespace StoreManager.Infrastructure.Invoice.Import.Model
{
    public class ImportItemModel
    {
        public required ImportModel Import { get; set; }
        public required MechanicalComponentModel Component { get; set; }
        public required int Quantity { get; set; }
        public required double PricePerPiece { get; set; }
        public required Guid ImportId { get; set; }
        public required Guid ComponentId { get; set; }
    }
}
