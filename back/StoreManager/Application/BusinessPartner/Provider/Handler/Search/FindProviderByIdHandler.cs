using MediatR;
using StoreManager.Application.BusinessPartner.Provider.Command.Search;
using StoreManager.Application.BusinessPartner.Provider.DTO.Search;
using StoreManager.Application.BusinessPartner.Provider.Repository;

namespace StoreManager.Application.BusinessPartner.Provider.Handler.Search
{
    public class FindProviderByIdHandler(IProviderRepository providerRepository)
        : IRequestHandler<FindProviderByIdQuery, ProviderFindResponseDto?>
    {
        public async Task<ProviderFindResponseDto?> Handle(FindProviderByIdQuery request, CancellationToken cancellationToken)
        {
            if (Guid.TryParse(request.Id, out _))
            {
                throw new InvalidCastException("Could not cast guid!");
            }
            var providerGuid = Guid.Parse(request.Id);
            var provider = await providerRepository.FindByIdAsync(providerGuid);
            if (provider is null) { return null; }

            return new ProviderFindResponseDto(provider.Id, provider.Name, provider.Address, provider.PhoneNumber);
        }
    }
}
