using MediatR;
using StoreManager.Application.MechanicalComponent.Command.Info;
using StoreManager.Application.MechanicalComponent.DTO.Info;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.MechanicalComponent.Handler.Info
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
            var component = await repository.FindByIdAsync(componentGuid);
            if (component is null)
            {
                throw new NotFoundException("Component not found");
            }
            var quantity = await repository.CountQuantityAsync(component);
            return new MechanicalComponentInfoResponseDto(
                component.Name,
                component.Identifier,
                quantity,
                component.Items.Select(ii => new MechanicalComponentInfoInvoiceResponseDto(
                    ii.Import.Id,
                    ii.Import.DateIssued,
                    new MechanicalComponentInfoProviderResponseDto(ii.Import.Provider.Id, ii.Import.Provider.Name, Utils.FormatAddress(ii.Import.Provider.Address) , ii.Import.Provider.PhoneNumber))
                ).ToList()
            );
        }
    }
}
