using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.Command;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Info;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using System.ComponentModel;

namespace StoreManager.Infrastructure.MechanicalComponent.Handler
{
    public class FindComponentInfoHandler : IRequestHandler<FindComponentInfoQuery, MechanicalComponentInfoResponseDTO>
    {
        private IMechanicalComponentRepository _repository;
        public FindComponentInfoHandler(IMechanicalComponentRepository repository) { _repository = repository; }

        public async Task<MechanicalComponentInfoResponseDTO> Handle(FindComponentInfoQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.ComponentId, out _))
            {
                throw new InvalidCastException("Guid cannot be parsed");
            }
            Guid componentGuid = Guid.Parse(request.ComponentId);
            var component = await _repository.FindById(componentGuid);
            if (component is null)
            {
                throw new EntryPointNotFoundException("Component not found");
            }
            var quantity = await _repository.CountQuantity(component);
            return new MechanicalComponentInfoResponseDTO(
                component.Name,
                component.Identifier,
                quantity,
                component.Items.Select(ii => new MechanicalComponentInfoInvoiceResponseDTO(
                    ii.Invoice.Id,
                    ii.Invoice.DateIssued,
                    new MechanicalComponentInfoProviderResponseDTO(ii.Invoice.Provider.Id, ii.Invoice.Provider.Name, ii.Invoice.Provider.Adress, ii.Invoice.Provider.PhoneNumber))
                ).ToList()
            );
        }
    }
}
