using MediatR;
using StoreManager.Infrastructure.Provider.Command;
using StoreManager.Infrastructure.Provider.DTO.Search;
using StoreManager.Infrastructure.Provider.Repository;

namespace StoreManager.Infrastructure.Provider.Handler
{
    public class CreateProviderHandler(IProviderRepository providerRepository)
        : IRequestHandler<CreateProviderCommand, ProviderFindResponseDto>
    {
        public async Task<ProviderFindResponseDto> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
        {
            var saved = await providerRepository.Create(new Model.ProviderModel
            {
                Adress = request.Address,
                Id = new Guid(),
                Name = request.Name,
                PhoneNumber = request.PhoneNumber
            });
            return new ProviderFindResponseDto(saved.Id, saved.Name, saved.Adress, saved.PhoneNumber);
        }
    }
}
