using MediatR;
using StoreManager.Application.BusinessPartner.Exporter.Command;
using StoreManager.Application.BusinessPartner.Exporter.Repository;
using StoreManager.Application.Common;
using StoreManager.Domain;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Exporter.Model;
using StoreManager.Domain.BusinessPartner.Shared;
using StoreManager.Domain.Invoice.Export.Model;

namespace StoreManager.Application.BusinessPartner.Exporter.Handler;

public class CreateExporterCommandHandler(IUnitOfWork unitOfWork, IExporterRepository repository)
    : IRequestHandler<CreateExporterCommand,Result>
{
    public async Task<Result> Handle(CreateExporterCommand request, CancellationToken cancellationToken)
    {
        var exporter = new Domain.BusinessPartner.Exporter.Model.Exporter
        {
            Address = new Address("c", "c", "C", 1, 4),
            Id = Guid.NewGuid(),
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            Type = BusinessPartnerType.Exporter,
            Exports = new List<Export>()
        };
        await repository.CreateAsync(exporter);
        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }
}