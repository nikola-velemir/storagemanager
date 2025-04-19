using MediatR;
using StoreManager.Application.BusinessPartner.Exporter.Command;
using StoreManager.Application.BusinessPartner.Exporter.Repository;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Exporter.Model;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Application.BusinessPartner.Exporter.Handler;

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