using System.Runtime.InteropServices.JavaScript;
using MediatR;
using StoreManager.Infrastructure.MiddleWare.Exceptions;
using StoreManager.Infrastructure.Provider.Command;
using StoreManager.Infrastructure.Provider.DTO.Search;
using StoreManager.Infrastructure.Provider.Repository;

namespace StoreManager.Infrastructure.Provider.Handler
{
    public class CreateProviderHandler(IProviderRepository providerRepository)
        : IRequestHandler<CreateProviderCommand, ProviderFindResponseDto>
    {
        public async Task<ProviderFindResponseDto> Handle(CreateProviderCommand request,
            CancellationToken cancellationToken)
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