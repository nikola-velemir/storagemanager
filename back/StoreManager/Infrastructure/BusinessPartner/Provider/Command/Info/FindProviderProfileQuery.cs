using MediatR;
using StoreManager.Infrastructure.BusinessPartner.Provider.DTO;

namespace StoreManager.Infrastructure.BusinessPartner.Provider.Command.Info
{
    public record FindProviderProfileQuery(string ProviderId) : IRequest<ProviderProfileResponseDto>;
}
