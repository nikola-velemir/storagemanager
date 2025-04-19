using MediatR;
using StoreManager.Application.Invoice.Import.Command;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Infrastructure.Invoice.Import.Repository;

namespace StoreManager.Application.Invoice.Import.Handler
{
    public class CreateImportFromDocumentCommandHandler(
        IImportRepository importRepository,
        IImportItemRepository importItemRepository,
        IMechanicalComponentRepository mechanicalComponentRepository)
        : IRequestHandler<CreateImportFromDocumentCommand>
    {
        private readonly IImportRepository _importRepository = importRepository;
        private readonly IImportItemRepository _importItemRepository = importItemRepository;
        private readonly IMechanicalComponentRepository _mechanicalComponentRepository = mechanicalComponentRepository;

        public Task<Unit> Handle(CreateImportFromDocumentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
