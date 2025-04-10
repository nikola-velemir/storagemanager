using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.Provider.DTO;
using StoreManager.Infrastructure.Provider.DTO.Info;
using StoreManager.Infrastructure.Provider.DTO.Search;
using StoreManager.Infrastructure.Provider.DTO.Statistics;
using StoreManager.Infrastructure.Provider.Repository;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Provider.Service
{
    public class ProviderService(
        IProviderRepository repository,
        IMechanicalComponentRepository mechanicalComponentRepository,
        IInvoiceRepository invoiceRepository)
        : IProviderService
    {
        public async Task<ProviderFindResponseDto> Create(ProviderCreateRequestDto request)
        {
            var saved = await repository.Create(new Model.ProviderModel
            {
                Adress = request.address,
                Id = new Guid(),
                Name = request.name,
                PhoneNumber = request.phoneNumber
            });
            return new ProviderFindResponseDto(saved.Id, saved.Name, saved.Adress, saved.PhoneNumber);
        }

        public async Task<ProviderFindResponsesDto> FindAll()
        {
            var providers = await repository.FindAll();
            var responses = providers.Select(p => new ProviderFindResponseDto(p.Id, p.Name, p.Adress, p.PhoneNumber)).ToList();
            return new ProviderFindResponsesDto(responses);
        }

        public async Task<ProviderFindResponseDto?> FindById(Guid id)
        {
            var provider = await repository.FindById(id);
            if (provider is null) { return null; }

            return new ProviderFindResponseDto(provider.Id, provider.Name, provider.Adress, provider.PhoneNumber);
        }

        public async Task<PaginatedResult<ProviderSearchResponseDto>> FindFiltered(string? providerName, int pageNumber, int pageSize)
        {
            var pr = await repository.FindById(Guid.Parse("2a2986ec-3206-4379-a26e-5f06b58b90aa"));
            var result = await repository.FindFiltered(providerName, pageNumber, pageSize);
            return new PaginatedResult<ProviderSearchResponseDto>
            {
                Items = result.Items.Select(p =>
                new ProviderSearchResponseDto(
                    p.Id,
                    p.Name,
                    p.Adress,
                    p.PhoneNumber,
                    p.Invoices.Select(i => new ProviderInvoiceSearchResponseDto(
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
            var provider = await repository.FindById(providerGuid);
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
                    components.Select(c => new ProviderProfileComponentResponseDto(c.Id,c.Name,c.Identifier)).ToList(),
                    invoices.Select(i => new ProviderProfileInvoiceResponseDto(i.Id, i.DateIssued)).ToList()
                );
        }

        public async Task<ProviderComponentInvolvementResponsesDto> FindProviderComponentInvolements()
        {
            var providers = await repository.FindAll();
            var responses = new List<ProviderComponentInvolvementResponseDto>();
            foreach(var provider in providers)
            {
                var count = await repository.FindComponentCountForProvider(provider);
                responses.Add(new ProviderComponentInvolvementResponseDto(provider.Id, provider.Name, count));

            }
            return new ProviderComponentInvolvementResponsesDto(responses);
        }

        public async Task<ProviderInvoiceInvolvementResponsesDto> FindProviderInvoiceInvolements()
        {
            var providers = await repository.FindAll();
            var responses = new List<ProviderInvoiceInvolvementResponseDto>();
            foreach(var provider in providers)
            {
                var count = await repository.FindInvoiceCountForProvider(provider);
                responses.Add(new ProviderInvoiceInvolvementResponseDto(provider.Id, provider.Name, count));
            }
            return new ProviderInvoiceInvolvementResponsesDto(responses);
        }
    }
}
