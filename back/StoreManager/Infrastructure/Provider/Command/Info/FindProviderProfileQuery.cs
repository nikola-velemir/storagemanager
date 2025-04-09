using MediatR;
using StoreManager.Infrastructure.Provider.DTO;

namespace StoreManager.Infrastructure.Provider.Command.Info
{
    public record FindProviderProfileQuery(string ProviderId) : IRequest<ProviderProfileResponseDTO>;
}
