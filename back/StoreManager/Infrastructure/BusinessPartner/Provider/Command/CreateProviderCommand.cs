using MediatR;
using StoreManager.Infrastructure.BusinessPartner.Provider.DTO.Search;

namespace StoreManager.Infrastructure.BusinessPartner.Provider.Command
{
    public record CreateProviderCommand(string Name, string Address, string PhoneNumber) : IRequest<ProviderFindResponseDto>;
}
