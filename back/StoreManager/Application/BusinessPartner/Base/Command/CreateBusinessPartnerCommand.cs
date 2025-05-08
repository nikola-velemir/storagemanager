using MediatR;
using StoreManager.Application.Common;

namespace StoreManager.Application.BusinessPartner.Base.Command;

public record CreateBusinessPartnerCommand(string Name, string PhoneNumber, string Role,string City, string Street, string StreetNumber) : IRequest<Result>;