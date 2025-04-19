using MediatR;
using StoreManager.Application.Invoice.Import.Command;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Invoice.Import.Repository;

namespace StoreManager.Application.Invoice.Import.Handler
{
    public class CreateImportCommandHandler(
        IImportItemRepository importItemRepository,
        IMechanicalComponentRepository mechanicalComponentRepository,
        IImportRepository importRepository)
        : IRequestHandler<CreateImportCommand>
    {
        public async Task<Unit> Handle(CreateImportCommand request, CancellationToken cancellationToken)
        {
            var invoice = await importRepository.FindByDocumentId(request.DocumentId);

            if (invoice is null) return Unit.Value;

            var components = await mechanicalComponentRepository.CreateFromExtractionMetadata(request.Metadata);
            foreach (var data in request.Metadata)
            {
                var component = await mechanicalComponentRepository.FindByIdentifier(data.Identifier);
                if (component is null) { continue; }
                var foundItem = await importItemRepository.FindByImportAndComponentId(invoice.Id, component.Id);
                if (foundItem is null)
                    await importItemRepository.Create(new ImportItemModel { Component = component, ComponentId = component.Id, Import = invoice, ImportId = invoice.Id, PricePerPiece = data.Price, Quantity = data.Quantity });
            }
            return Unit.Value;
        }
    }
}
