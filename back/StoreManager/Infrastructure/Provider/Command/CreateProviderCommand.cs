using MediatR;
using StoreManager.Infrastructure.Provider.DTO;

namespace StoreManager.Infrastructure.Provider.Command
{
    public record CreateProviderCommand(ProviderCreateRequestDTO Request) : IRequest<ProviderFindResponseDTO>;
}
