using MediatR;
using StoreManager.Infrastructure.Provider.Command;
using StoreManager.Infrastructure.Provider.DTO.Search;
using StoreManager.Infrastructure.Provider.Repository;

namespace StoreManager.Infrastructure.Provider.Handler
{
    public class CreateProviderHandler : IRequestHandler<CreateProviderCommand, ProviderFindResponseDTO>
    {
        private IProviderRepository _providerRepository;
        public CreateProviderHandler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<ProviderFindResponseDTO> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
        {
            var saved = await _providerRepository.Create(new Model.ProviderModel
            {
                Adress = request.Address,
                Id = new Guid(),
                Name = request.Name,
                PhoneNumber = request.PhoneNumber
            });
            return new ProviderFindResponseDTO(saved.Id, saved.Name, saved.Adress, saved.PhoneNumber);
        }
    }
}
