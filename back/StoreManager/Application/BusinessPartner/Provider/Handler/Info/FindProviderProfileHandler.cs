using MediatR;
using StoreManager.Application.BusinessPartner.Provider.Command.Info;
using StoreManager.Application.BusinessPartner.Provider.DTO;
using StoreManager.Application.BusinessPartner.Provider.DTO.Info;
using StoreManager.Application.BusinessPartner.Provider.Repository;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Infrastructure.Invoice.Import.Repository.Specification;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.BusinessPartner.Provider.Handler.Info
{
    public class FindProviderProfileHandler(
        IMechanicalComponentRepository mechanicalComponentRepository,
        IProviderRepository providerRepository,
        IImportRepository importRepository)
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
                throw new NotFoundException("Provider not found");
            }
            var components = await mechanicalComponentRepository.FindByProviderId(provider.Id);
            var invoices = await importRepository.FindByProviderId(new ImportWithProvider(),provider.Id);

            return
                new ProviderProfileResponseDto(
                    provider.Name,
                    provider.Address,
                    provider.PhoneNumber,
                    components.Select(c => new ProviderProfileComponentResponseDto(c.Id, c.Name, c.Identifier)).ToList(),
                    invoices.Select(i => new ProviderProfileInvoiceResponseDto(i.Id, i.DateIssued)).ToList()
                );
        }
    }
}
