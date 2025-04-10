using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.Command.Info;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Info;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using System.ComponentModel;

namespace StoreManager.Infrastructure.MechanicalComponent.Handler.Info
{
    public class FindComponentInfoHandler(IMechanicalComponentRepository repository)
        : IRequestHandler<FindComponentInfoQuery, MechanicalComponentInfoResponseDto>
    {
        public async Task<MechanicalComponentInfoResponseDto> Handle(FindComponentInfoQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.ComponentId, out _))
            {
                throw new InvalidCastException("Guid cannot be parsed");
            }
            Guid componentGuid = Guid.Parse(request.ComponentId);
            var component = await repository.FindById(componentGuid);
            if (component is null)
            {
                throw new EntryPointNotFoundException("Component not found");
            }
            var quantity = await repository.CountQuantity(component);
            return new MechanicalComponentInfoResponseDto(
                component.Name,
                component.Identifier,
                quantity,
                component.Items.Select(ii => new MechanicalComponentInfoInvoiceResponseDto(
                    ii.Invoice.Id,
                    ii.Invoice.DateIssued,
                    new MechanicalComponentInfoProviderResponseDto(ii.Invoice.Provider.Id, ii.Invoice.Provider.Name, ii.Invoice.Provider.Adress, ii.Invoice.Provider.PhoneNumber))
                ).ToList()
            );
        }
    }
}
