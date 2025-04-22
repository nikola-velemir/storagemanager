using StoreManager.Application.BusinessPartner.Provider.DTO;
using StoreManager.Application.BusinessPartner.Provider.DTO.Info;
using StoreManager.Application.BusinessPartner.Provider.DTO.Search;
using StoreManager.Application.BusinessPartner.Provider.DTO.Statistics;
using StoreManager.Application.BusinessPartner.Provider.Repository;
using StoreManager.Application.GeoCoding;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Application.Shared;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Domain.BusinessPartner.Provider.Service;
using StoreManager.Domain.BusinessPartner.Shared;
using StoreManager.Domain.Invoice.Import.Specification;
using StoreManager.Infrastructure.Invoice.Import.Repository.Specification;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.BusinessPartner.Provider.Service
{
    public class ProviderService(
        IProviderRepository repository,
        IMechanicalComponentRepository mechanicalComponentRepository,
        IImportRepository importRepository
        )
        : IProviderService
    {
        public async Task<ProviderFindResponseDto> Create(ProviderCreateRequestDto request)
        {
            var saved = await repository.CreateAsync(new ProviderModel
            {
                Address = new Address("AA", "AA", "AA", 0.4, 0.4),
                Id = Guid.NewGuid(),
                Name = request.Name,
                Type = BusinessPartnerType.Provider,
                PhoneNumber = request.PhoneNumber
            });
            return new ProviderFindResponseDto(saved.Id, saved.Name, Utils.FormatAddress(saved.Address), saved.PhoneNumber);
        }

        public async Task<ProviderFindResponsesDto> FindAll()
        {
            var providers = await repository.FindAllAsync();
            var responses = providers.Select(p =>
                new ProviderFindResponseDto(p.Id, p.Name, Utils.FormatAddress(p.Address), p.PhoneNumber)).ToList();
            return new ProviderFindResponsesDto(responses);
        }

        public async Task<ProviderFindResponseDto?> FindById(Guid id)
        {
            var provider = await repository.FindByIdAsync(id);
            if (provider is null)
            {
                return null;
            }

            return new ProviderFindResponseDto(provider.Id, provider.Name, Utils.FormatAddress(provider.Address),
                provider.PhoneNumber);
        }

        public async Task<PaginatedResult<ProviderSearchResponseDto>> FindFiltered(string? providerName, int pageNumber,
            int pageSize)
        {
            var pr = await repository.FindByIdAsync(Guid.Parse("2a2986ec-3206-4379-a26e-5f06b58b90aa"));
            var result = await repository.FindFilteredAsync(providerName, pageNumber, pageSize);
            return new PaginatedResult<ProviderSearchResponseDto>
            {
                Items = result.Items.Select(p =>
                    new ProviderSearchResponseDto(
                        p.Id,
                        p.Name,
                        Utils.FormatAddress(p.Address),
                        p.PhoneNumber,
                        p.Imports.Select(i => new ProviderInvoiceSearchResponseDto(
                            i.Id,
                            i.DateIssued)
                        ).ToList()
                    )
                ).ToList()
            };
        }

        public async Task<ProviderProfileResponseDto> FindProfile(string providerId)
        {
            if (!Guid.TryParse(providerId, out _))
            {
                throw new InvalidCastException("Could not parse the guid");
            }

            Guid providerGuid = Guid.Parse(providerId);
            var provider = await repository.FindByIdAsync(providerGuid);
            if (provider is null)
            {
                throw new NotFoundException("Provider not found");
            }

            var components = await mechanicalComponentRepository.FindByProviderIdAsync(provider.Id);
            var invoices = await importRepository.FindByProviderId(new ImportWithProvider(), provider.Id);

            return
                new ProviderProfileResponseDto(
                    provider.Name,
                    Utils.FormatAddress(provider.Address),
                    provider.PhoneNumber,
                    components.Select(c => new ProviderProfileComponentResponseDto(c.Id, c.Name, c.Identifier))
                        .ToList(),
                    invoices.Select(i => new ProviderProfileInvoiceResponseDto(i.Id, i.DateIssued)).ToList()
                );
        }

        public async Task<ProviderComponentInvolvementResponsesDto> FindProviderComponentInvolements()
        {
            var providers = await repository.FindAllAsync();
            var responses = new List<ProviderComponentInvolvementResponseDto>();
            foreach (var provider in providers)
            {
                var count = await repository.FindComponentCountForProviderAsync(provider);
                responses.Add(new ProviderComponentInvolvementResponseDto(provider.Id, provider.Name, count));
            }

            return new ProviderComponentInvolvementResponsesDto(responses);
        }

        public async Task<ProviderInvoiceInvolvementResponsesDto> FindProviderInvoiceInvolements()
        {
            var providers = await repository.FindAllAsync();
            var responses = new List<ProviderInvoiceInvolvementResponseDto>();
            foreach (var provider in providers)
            {
                var count = await repository.FindInvoiceCountForProviderAsync(provider);
                responses.Add(new ProviderInvoiceInvolvementResponseDto(provider.Id, provider.Name, count));
            }

            return new ProviderInvoiceInvolvementResponsesDto(responses);
        }
    }
}