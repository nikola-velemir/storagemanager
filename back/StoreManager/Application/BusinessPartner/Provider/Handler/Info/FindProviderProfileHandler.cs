using MediatR;
using StoreManager.Application.BusinessPartner.Base.Errors;
using StoreManager.Application.BusinessPartner.Provider.Command.Info;
using StoreManager.Application.BusinessPartner.Provider.DTO;
using StoreManager.Application.BusinessPartner.Provider.DTO.Info;
using StoreManager.Application.BusinessPartner.Provider.Repository;
using StoreManager.Application.Common;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Domain.Invoice.Import.Specification;
using StoreManager.Infrastructure.Invoice.Import.Repository.Specification;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.BusinessPartner.Provider.Handler.Info
{
    public class FindProviderProfileHandler(
        IMechanicalComponentRepository mechanicalComponentRepository,
        IProviderRepository providerRepository,
        IImportRepository importRepository)
        : IRequestHandler<FindProviderProfileQuery, Result<ProviderProfileResponseDto>>
    {
        public async Task<Result<ProviderProfileResponseDto>> Handle(FindProviderProfileQuery request,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.ProviderId, out _))
            {
                return BusinessPartnerErrors.PartnerIdParseError;
            }

            var providerGuid = Guid.Parse(request.ProviderId);
            var provider = await providerRepository.FindByIdAsync(providerGuid);
            if (provider is null)
            {
                return BusinessPartnerErrors.PartnerNotFoundError;
            }

            var components = await mechanicalComponentRepository.FindByProviderIdAsync(provider.Id);
            var invoices = await importRepository.FindByProviderId(new ImportWithProvider(), provider.Id);

            var response = new ProviderProfileResponseDto(
                provider.Name,
                Utils.FormatAddress(provider.Address),
                provider.PhoneNumber,
                components.Select(c => new ProviderProfileComponentResponseDto(c.Id, c.Name, c.Identifier))
                    .ToList(),
                invoices.Select(i => new ProviderProfileInvoiceResponseDto(i.Id, i.DateIssued)).ToList()
            );
            return Result.Success(response);

        }
    }
}