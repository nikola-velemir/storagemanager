using MediatR;
using StoreManager.Application.BusinessPartner.Provider.DTO;
using StoreManager.Application.Common;

namespace StoreManager.Application.BusinessPartner.Provider.Command.Info
{
    public record FindProviderProfileQuery(string ProviderId) : IRequest<Result<ProviderProfileResponseDto>>;
}
