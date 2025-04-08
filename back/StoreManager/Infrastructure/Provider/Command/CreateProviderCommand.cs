using MediatR;
using StoreManager.Infrastructure.Provider.DTO;

namespace StoreManager.Infrastructure.Provider.Command
{
    public record CreateProviderCommand(string Name, string Address, string PhoneNumber) : IRequest<ProviderFindResponseDTO>;
}
