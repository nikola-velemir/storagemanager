using MediatR;
using StoreManager.Application.BusinessPartner.Provider.DTO;

namespace StoreManager.Application.BusinessPartner.Provider.Command.Info
{
    public record FindProviderProfileQuery(string ProviderId) : IRequest<ProviderProfileResponseDto>;
}
