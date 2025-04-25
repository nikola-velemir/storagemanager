using MediatR;
using StoreManager.Application.Invoice.Import.Command;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Domain;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Invoice.Import.Repository;

namespace StoreManager.Application.Invoice.Import.Handler
{
    public class CreateImportCommandHandler(
        IUnitOfWork unitOfWork,
        IMechanicalComponentRepository mechanicalComponentRepository,
        IImportRepository importRepository)
        : IRequestHandler<CreateImportCommand>
    {
        public async Task<Unit> Handle(CreateImportCommand request, CancellationToken cancellationToken)
        {
            var invoice = await importRepository.FindByDocumentId(request.DocumentId);

            if (invoice is null) return Unit.Value;

            await mechanicalComponentRepository.CreateFromExtractionMetadataAsync(request.Metadata);
            foreach (var data in request.Metadata)
            {
                var component = await mechanicalComponentRepository.FindByIdentifierAsync(data.Identifier);
                if (component is null)
                {
                    continue;
                }

                var foundItem = await importRepository.FindByImportAndComponentIdAsync(invoice.Id, component.Id);
                if (foundItem is not null) continue;
                
                invoice.AddItem(new ImportItemModel
                {
                    Component = component,
                    ComponentId = component.Id, 
                    Import = invoice,
                    ImportId = invoice.Id,
                    PricePerPiece = data.Price,
                    Quantity = data.Quantity
                });
//                await importRepository.UpdateAsync(invoice);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}