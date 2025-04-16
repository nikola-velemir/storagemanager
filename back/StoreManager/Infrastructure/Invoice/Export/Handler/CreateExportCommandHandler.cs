using MediatR;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Repository;
using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Command;
using StoreManager.Infrastructure.Invoice.Export.Model;
using StoreManager.Infrastructure.Invoice.Export.Repository;

namespace StoreManager.Infrastructure.Invoice.Handler;

public class CreateExportCommandHandler(IExporterRepository exporterRepository, IExportRepository exportRepository) : IRequestHandler<CreateExportCommand>
{
    public async Task<Unit> Handle(CreateExportCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.ProviderId, out _))
        {
            throw new ArgumentException("Invalid provider id");
        }

        var exporterId = Guid.Parse(request.ProviderId);
        var exporter = await exporterRepository.FindById((exporterId));

        var export = exportRepository.Create(new ExportModel
        {
            Document = null,
            Exporter = exporter,
            Id = Guid.NewGuid(),
            DateIssued = DateOnly.FromDateTime(DateTime.UtcNow),
            DocumentId = Guid.Empty,
            ExporterId = exporter.Id,
            Type = InvoiceType.Export,
            Items = new List<ExportItemModel>()
        });
        return Unit.Value;
    }
}