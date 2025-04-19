using MediatR;
using StoreManager.Application.BusinessPartner.Provider.DTO.Search;

namespace StoreManager.Domain.BusinessPartner.Provider.Command
{
    public record CreateProviderCommand(string Name, string Address, string PhoneNumber) : IRequest<ProviderFindResponseDto>;
}
