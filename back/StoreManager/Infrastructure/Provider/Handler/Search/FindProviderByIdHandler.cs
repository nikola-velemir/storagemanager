using DocumentFormat.OpenXml.Office2010.Excel;
using MediatR;
using StoreManager.Infrastructure.Provider.Command.Search;
using StoreManager.Infrastructure.Provider.DTO.Search;
using StoreManager.Infrastructure.Provider.Repository;

namespace StoreManager.Infrastructure.Provider.Handler.Search
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
            var provider = await providerRepository.FindById(providerGuid);
            if (provider is null) { return null; }

            return new ProviderFindResponseDto(provider.Id, provider.Name, provider.Adress, provider.PhoneNumber);
        }
    }
}
