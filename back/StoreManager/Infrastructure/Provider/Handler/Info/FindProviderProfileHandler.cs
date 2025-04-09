using MediatR;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.Provider.Command.Info;
using StoreManager.Infrastructure.Provider.DTO;
using StoreManager.Infrastructure.Provider.DTO.Info;
using StoreManager.Infrastructure.Provider.Repository;
using static Supabase.Gotrue.Constants;

namespace StoreManager.Infrastructure.Provider.Handler.Info
{
    public class FindProviderProfileHandler : IRequestHandler<FindProviderProfileQuery, ProviderProfileResponseDTO>
    {
        private IMechanicalComponentRepository _mechanicalComponentRepository;
        private IProviderRepository _providerRepository;
        private IInvoiceRepository _invoiceRepository;

        public FindProviderProfileHandler(IMechanicalComponentRepository mechanicalComponentRepository, IProviderRepository providerRepository, IInvoiceRepository invoiceRepository)
        {
            _mechanicalComponentRepository = mechanicalComponentRepository;
            _providerRepository = providerRepository;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<ProviderProfileResponseDTO> Handle(FindProviderProfileQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.ProviderId, out _))
            {
                throw new InvalidCastException("Could not parse the guid");
            }
            Guid providerGuid = Guid.Parse(request.ProviderId);
            var provider = await _providerRepository.FindById(providerGuid);
            if (provider is null)
            {
                throw new EntryPointNotFoundException("Provider not found");
            }
            var components = await _mechanicalComponentRepository.FindByProviderId(provider.Id);
            var invoices = await _invoiceRepository.FindByProviderId(provider.Id);

            return
                new ProviderProfileResponseDTO(
                    provider.Name,
                    provider.Adress,
                    provider.PhoneNumber,
                    components.Select(c => new ProviderProfileComponentResponseDTO(c.Id, c.Name, c.Identifier)).ToList(),
                    invoices.Select(i => new ProviderProfileInvoiceResponseDTO(i.Id, i.DateIssued)).ToList()
                );
        }
    }
}
