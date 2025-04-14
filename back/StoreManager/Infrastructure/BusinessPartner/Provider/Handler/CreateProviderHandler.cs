using MediatR;
using StoreManager.Infrastructure.BusinessPartner.Base;
using StoreManager.Infrastructure.BusinessPartner.Provider.Command;
using StoreManager.Infrastructure.BusinessPartner.Provider.DTO.Search;
using StoreManager.Infrastructure.BusinessPartner.Provider.Model;
using StoreManager.Infrastructure.BusinessPartner.Provider.Repository;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Infrastructure.BusinessPartner.Provider.Handler
{
    public class CreateProviderHandler(IProviderRepository providerRepository)
        : IRequestHandler<CreateProviderCommand, ProviderFindResponseDto>
    {
        public async Task<ProviderFindResponseDto> Handle(CreateProviderCommand request,
            CancellationToken cancellationToken)
        {
            var saved = await providerRepository.Create(new ProviderModel
            {
                Address = request.Address,
                Id = Guid.NewGuid(),
                Name = request.Name,
                Type = BusinessPartnerType.Provider,
                PhoneNumber = request.PhoneNumber
            });
            return new ProviderFindResponseDto(saved.Id, saved.Name, saved.Address, saved.PhoneNumber);
        }

        private static void ValidateRequest(CreateProviderCommand request)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(request.Address) || string.IsNullOrWhiteSpace(request.Address))
                errors.Add("Address is required");
            if(string.IsNullOrEmpty(request.PhoneNumber) || string.IsNullOrWhiteSpace(request.PhoneNumber))
                errors.Add("Phone number is required");
            if(string.IsNullOrEmpty(request.Name) || string.IsNullOrWhiteSpace(request.Name))
                errors.Add("Name is required");

            if (errors.Count != 0)
                throw new ValidationException(string.Join(" ", errors));
        }
    }
}