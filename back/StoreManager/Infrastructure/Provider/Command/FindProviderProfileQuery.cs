using MediatR;
using StoreManager.Infrastructure.Provider.DTO;

namespace StoreManager.Infrastructure.Provider.Command
{
    public record FindProviderProfileQuery(string ProviderId) : IRequest<ProviderProfileResponseDTO>;
}
