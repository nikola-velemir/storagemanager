using MediatR;
using StoreManager.Infrastructure.BusinessPartner.Base;
using StoreManager.Infrastructure.BusinessPartner.Base.Model;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Command;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Model;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Repository;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.BusinessPartner.Exporter.Handler;

public class CreateExporterCommandHandler(IExporterRepository repository) : IRequestHandler<CreateExporterCommand>
{
    public async Task<Unit> Handle(CreateExporterCommand request, CancellationToken cancellationToken)
    {
        var exporter = new ExporterModel
        {
            Address = request.Address,
            Id = Guid.NewGuid(),
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            Type = BusinessPartnerType.Exporter,
            Exports = new List<ExportModel>()
        };
        await repository.Create(exporter);
        return Unit.Value;
    }
}