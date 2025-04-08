using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.Provider.DTO;
using StoreManager.Infrastructure.Provider.Repository;
using StoreManager.Infrastructure.Shared;
using UglyToad.PdfPig.Graphics.Operations.PathConstruction;

namespace StoreManager.Infrastructure.Provider.Service
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMechanicalComponentRepository _mechanicalComponentRepository;
        public ProviderService(IProviderRepository repository, IMechanicalComponentRepository mechanicalComponentRepository, IInvoiceRepository invoiceRepository)
        {
            _mechanicalComponentRepository = mechanicalComponentRepository;
            _providerRepository = repository;
            _invoiceRepository = invoiceRepository;
        }
        public async Task<ProviderFindResponseDTO> Create(ProviderCreateRequestDTO request)
        {
            var saved = await _providerRepository.Create(new Model.ProviderModel
            {
                Adress = request.address,
                Id = new Guid(),
                Name = request.name,
                PhoneNumber = request.phoneNumber
            });
            return new ProviderFindResponseDTO(saved.Id, saved.Name, saved.Adress, saved.PhoneNumber);
        }

        public async Task<ProviderFindResponsesDTO> FindAll()
        {
            var providers = await _providerRepository.FindAll();
            var responses = providers.Select(p => new ProviderFindResponseDTO(p.Id, p.Name, p.Adress, p.PhoneNumber)).ToList();
            return new ProviderFindResponsesDTO(responses);
        }

        public async Task<ProviderFindResponseDTO?> FindById(Guid id)
        {
            var provider = await _providerRepository.FindById(id);
            if (provider is null) { return null; }

            return new ProviderFindResponseDTO(provider.Id, provider.Name, provider.Adress, provider.PhoneNumber);
        }

        public async Task<PaginatedResult<ProviderSearchResponseDTO>> FindFiltered(string? providerName, int pageNumber, int pageSize)
        {
            var pr = await _providerRepository.FindById(Guid.Parse("2a2986ec-3206-4379-a26e-5f06b58b90aa"));
            var result = await _providerRepository.FindFiltered(providerName, pageNumber, pageSize);
            return new PaginatedResult<ProviderSearchResponseDTO>
            {
                Items = result.Items.Select(p =>
                new ProviderSearchResponseDTO(
                    p.Id,
                    p.Name,
                    p.Adress,
                    p.PhoneNumber,
                    p.Invoices.Select(i => new ProviderInvoiceSearchResponseDTO(
                        i.Id,
                        i.DateIssued)
                    ).ToList()
                )
            ).ToList()
            };
        }

        public async Task<ProviderProfileResponseDTO> FindProfile(string providerId)
        {
            if (!Guid.TryParse(providerId, out _))
            {
                throw new InvalidCastException("Could not parse the guid");
            }
            Guid providerGuid = Guid.Parse(providerId);
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
                    components.Select(c => new ProviderProfileComponentResponseDTO(c.Id,c.Name,c.Identifier)).ToList(),
                    invoices.Select(i => new ProviderProfileInvoiceResponseDTO(i.Id, i.DateIssued)).ToList()
                );
        }
    }
}
