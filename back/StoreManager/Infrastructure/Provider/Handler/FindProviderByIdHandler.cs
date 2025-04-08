using DocumentFormat.OpenXml.Office2010.Excel;
using MediatR;
using StoreManager.Infrastructure.Provider.Command;
using StoreManager.Infrastructure.Provider.DTO;
using StoreManager.Infrastructure.Provider.Repository;

namespace StoreManager.Infrastructure.Provider.Handler
{
    public class FindProviderByIdHandler : IRequestHandler<FindProviderByIdQuery, ProviderFindResponseDTO?>
    {
        private IProviderRepository _providerRepository;

        public FindProviderByIdHandler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<ProviderFindResponseDTO?> Handle(FindProviderByIdQuery request, CancellationToken cancellationToken)
        {
            if (Guid.TryParse(request.Id, out _))
            {
                throw new InvalidCastException("Could not cast guid!");
            }
            var providerGuid = Guid.Parse(request.Id);
            var provider = await _providerRepository.FindById(providerGuid);
            if (provider is null) { return null; }

            return new ProviderFindResponseDTO(provider.Id, provider.Name, provider.Adress, provider.PhoneNumber);
        }
    }
}
