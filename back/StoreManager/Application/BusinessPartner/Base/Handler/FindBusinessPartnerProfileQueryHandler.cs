using MediatR;
using StoreManager.Application.BusinessPartner.Base.Command;
using StoreManager.Application.BusinessPartner.Base.DTO;
using StoreManager.Application.BusinessPartner.Base.Errors;
using StoreManager.Application.BusinessPartner.Base.Repository;
using StoreManager.Application.Common;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.BusinessPartner.Base.Handler;

public class FindBusinessPartnerProfileQueryHandler(IBusinessPartnerRepository repository)
    : IRequestHandler<FindBusinessPartnerProfileQuery, Result<BusinessPartnerProfileResponseDto>>
{
    public async Task<Result<BusinessPartnerProfileResponseDto>> Handle(FindBusinessPartnerProfileQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var id))
        {
            return BusinessPartnerErrors.PartnerIdParseError;
        }

        var partner = await repository.FindById(id) ?? throw new NotFoundException("Partner not found");
        var address = partner.Address;
        var response =  new BusinessPartnerProfileResponseDto(partner.Name,
            new BusinessPartnerAddressResponseDto(address.City, address.Street, address.StreetNumber, address.Latitude,
                address.Longitude),
            partner.Type.ToString(), partner.PhoneNumber);
        return Result.Success(response);
    }
}