using StoreManager.Infrastructure.Provider.DTO;
using StoreManager.Infrastructure.Provider.Repository;
using UglyToad.PdfPig.Graphics.Operations.PathConstruction;

namespace StoreManager.Infrastructure.Provider.Service
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _repository;
        public ProviderService(IProviderRepository repository)
        {
            _repository = repository;
        }
        public async Task<ProviderFindResponseDTO> Create(ProviderCreateRequestDTO request)
        {
            var saved = await _repository.Create(new Model.ProviderModel
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
            var providers = await _repository.FindAll();
            var responses = providers.Select(p => new ProviderFindResponseDTO(p.Id, p.Name, p.Adress, p.PhoneNumber)).ToList();
            return new ProviderFindResponsesDTO(responses);
        }

        public async Task<ProviderFindResponseDTO?> FindById(Guid id)
        {
            var provider = await _repository.FindById(id);
            if (provider is null) { return null; }

            return new ProviderFindResponseDTO(provider.Id, provider.Name, provider.Adress, provider.PhoneNumber);
        }
    }
}
