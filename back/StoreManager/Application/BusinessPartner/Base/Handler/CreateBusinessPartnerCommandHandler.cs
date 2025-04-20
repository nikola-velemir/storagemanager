using MediatR;
using StoreManager.Application.BusinessPartner.Base.Command;
using StoreManager.Application.BusinessPartner.Base.Repository;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Exporter.Model;
using StoreManager.Domain.BusinessPartner.Provider.Model;

namespace StoreManager.Application.BusinessPartner.Base.Handler;

public class CreateBusinessPartnerCommandHandler(IBusinessPartnerRepository repository)
    : IRequestHandler<CreateBusinessPartnerCommand>
{
    public async Task<Unit> Handle(CreateBusinessPartnerCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<BusinessPartnerType>(request.Role, out _))
            throw new InvalidCastException("Could not cast to business partner");

        var role = Enum.Parse<BusinessPartnerType>(request.Role);

        BusinessPartnerModel partner = role switch
        {
            BusinessPartnerType.Exporter => new ExporterModel
            {
                Address = request.Address,
                Id = Guid.NewGuid(),
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Type = role
            },
            BusinessPartnerType.Provider => new ProviderModel
            {
                Address = request.Address,
                Id = Guid.NewGuid(),
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Type = role
            },
            _ => throw new ArgumentException()
        };
        
        await repository.CreateAsync(partner);
        return Unit.Value;
    }
}