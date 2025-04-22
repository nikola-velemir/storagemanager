using MediatR;
using StoreManager.Application.BusinessPartner.Base.Command;
using StoreManager.Application.BusinessPartner.Base.DTO;
using StoreManager.Application.BusinessPartner.Base.Repository;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.BusinessPartner.Base.Handler;

public class FindBusinessPartnerProfileQueryHandler(IBusinessPartnerRepository repository)
    : IRequestHandler<FindBusinessPartnerProfileQuery, BusinessPartnerProfileResponseDto>
{
    public async Task<BusinessPartnerProfileResponseDto> Handle(FindBusinessPartnerProfileQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var id))
        {
            throw new InvalidCastException();
        }

        var partner = await repository.FindById(id) ?? throw new NotFoundException("Partner not found");
        var address = partner.Address;
        return new BusinessPartnerProfileResponseDto(partner.Name,
            new BusinessPartnerAddressResponseDto(address.City, address.Street, address.StreetNumber, address.Latitude,
                address.Longitude),
            partner.Type.ToString(), partner.PhoneNumber);
    }
}