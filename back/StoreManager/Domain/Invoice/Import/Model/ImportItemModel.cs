using StoreManager.Domain.MechanicalComponent.Model;

namespace StoreManager.Infrastructure.Invoice.Import.Model
{
    public class ImportItemModel
    {
        public required Domain.Invoice.Import.Model.Import Import { get; set; }
        public required Domain.MechanicalComponent.Model.MechanicalComponent Component { get; set; }
        public required int Quantity { get; set; }
        public required double PricePerPiece { get; set; }
        public required Guid ImportId { get; set; }
        public required Guid ComponentId { get; set; }
    }
}
