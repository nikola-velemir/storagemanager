using MediatR;
using StoreManager.Application.BusinessPartner.Base.DTO;
using StoreManager.Application.Common;

namespace StoreManager.Application.BusinessPartner.Base.Command;

public record FindBusinessPartnerProfileQuery(string Id) : IRequest<Result<BusinessPartnerProfileResponseDto>>;
