using MediatR;
using StoreManager.Application.BusinessPartner.Base.DTO;

namespace StoreManager.Application.BusinessPartner.Base.Command;

public record FindBusinessPartnerProfileQuery(string Id) : IRequest<BusinessPartnerProfileResponseDto>;
