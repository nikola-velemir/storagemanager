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
    public class FindProviderProfileHandler(
        IMechanicalComponentRepository mechanicalComponentRepository,
        IProviderRepository providerRepository,
        IInvoiceRepository invoiceRepository)
        : IRequestHandler<FindProviderProfileQuery, ProviderProfileResponseDto>
    {
        public async Task<ProviderProfileResponseDto> Handle(FindProviderProfileQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.ProviderId, out _))
            {
                throw new InvalidCastException("Could not parse the guid");
            }
            Guid providerGuid = Guid.Parse(request.ProviderId);
            var provider = await providerRepository.FindById(providerGuid);
            if (provider is null)
            {
                throw new EntryPointNotFoundException("Provider not found");
            }
            var components = await mechanicalComponentRepository.FindByProviderId(provider.Id);
            var invoices = await invoiceRepository.FindByProviderId(provider.Id);

            return
                new ProviderProfileResponseDto(
                    provider.Name,
                    provider.Adress,
                    provider.PhoneNumber,
                    components.Select(c => new ProviderProfileComponentResponseDto(c.Id, c.Name, c.Identifier)).ToList(),
                    invoices.Select(i => new ProviderProfileInvoiceResponseDto(i.Id, i.DateIssued)).ToList()
                );
        }
    }
}
