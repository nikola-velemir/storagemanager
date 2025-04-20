using MediatR;

namespace StoreManager.Application.BusinessPartner.Base.Command;

public record CreateBusinessPartnerCommand(string Name, string Address, string PhoneNumber, string Role) : IRequest;