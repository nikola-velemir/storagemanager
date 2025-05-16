using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.MechanicalComponent.Command.Info;
using StoreManager.Application.MechanicalComponent.DTO.Info;
using StoreManager.Application.MechanicalComponent.Errors;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.MechanicalComponent.Handler.Info
{
    public class FindComponentInfoHandler(IMechanicalComponentRepository repository)
        : IRequestHandler<FindComponentInfoQuery, Result<MechanicalComponentInfoResponseDto>>
    {
        public async Task<Result<MechanicalComponentInfoResponseDto>> Handle(FindComponentInfoQuery request,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.ComponentId, out _))
            {
                return MechanicalComponentErrors.ComponentIdParseError;
            }

            var componentGuid = Guid.Parse(request.ComponentId);
            var component = await repository.FindByIdAsync(componentGuid);
            if (component is null)
            {
                return MechanicalComponentErrors.ComponentNotFound;
            }

            var quantity = await repository.CountQuantityAsync(component);
            var response = new MechanicalComponentInfoResponseDto(
                component.Name,
                component.Identifier,
                quantity,
                component.Items.Select(ii => new MechanicalComponentInfoInvoiceResponseDto(
                    ii.Import.Id,
                    ii.Import.DateIssued,
                    new MechanicalComponentInfoProviderResponseDto(ii.Import.Provider.Id, ii.Import.Provider.Name,
                        Utils.FormatAddress(ii.Import.Provider.Address), ii.Import.Provider.PhoneNumber))
                ).ToList()
            );
            return Result.Success(response);
        }
    }
}