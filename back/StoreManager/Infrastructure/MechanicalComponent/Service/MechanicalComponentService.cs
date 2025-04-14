using StoreManager.Infrastructure.MechanicalComponent.DTO.Find;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Info;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Quantity;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Search;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.MiddleWare.Exceptions;
using StoreManager.Infrastructure.Shared;
using System.Collections.Generic;

namespace StoreManager.Infrastructure.MechanicalComponent.Service
{
    public class MechanicalComponentService(IMechanicalComponentRepository repository) : IMechanicalComponentService
    {
        public async Task<MechanicalComponentFindResponsesDto> FindByInvoiceId(string invoiceId)
        {
            if (!Guid.TryParse(invoiceId, out _))
            {
                throw new InvalidCastException("Guid cannot be parsed");
            }
            Guid invoiceGuid = Guid.Parse(invoiceId);
            var result = await repository.FindByInvoiceId(invoiceGuid);

            return new MechanicalComponentFindResponsesDto(
                result.Select(mc =>
                new MechanicalComponentFindResponseDto(
                    mc.Id,
                    mc.Identifier,
                    mc.Name,
                    mc.Items.First(ii => ii.ImportId.Equals(invoiceGuid)).Quantity,
                    mc.Items.First(ii => ii.ImportId.Equals(invoiceGuid)).PricePerPiece
                    )
                ).ToList());
        }

        public async Task<PaginatedResult<MechanicalComponentSearchResponseDto>> FindFiltered(string? providerId, string? componentInfo, int pageNumber, int pageSize)
        {
            Guid? id = null;
            if (Guid.TryParse(providerId, out var tempId))
            {
                id = tempId;
            }

            var result = await repository.FindFiltered(id, componentInfo, pageNumber, pageSize);
            return new PaginatedResult<MechanicalComponentSearchResponseDto>
            {
                Items = result.Items.Select(mc =>
                new MechanicalComponentSearchResponseDto(
                    mc.Id,
                    mc.Identifier,
                    mc.Name,
                    mc.Items.Select(ii =>
                    new MechanicalComponentSearchInvoiceResponseDto(
                        ii.Import.Id,
                        ii.Import.DateIssued,
                       new MechanicalComponentSearchProviderResponseDto(
                           ii.Import.Provider.Id,
                           ii.Import.Provider.Name,
                           ii.Import.Provider.Address,
                           ii.Import.Provider.PhoneNumber
                           )
                       )).ToList()
                    )
                )
                .ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = result.TotalCount
            };
        }

        public async Task<PaginatedResult<MechanicalComponentProductSearchResponseDto>> FindFilteredForProduct(string? providerId, string? componentInfo, int pageNumber, int pageSize)
        {
            Guid? id = null;
            if (Guid.TryParse(providerId, out var tempId))
            {
                id = tempId;
            }

            var result = await repository.FindFilteredForProduct(id, componentInfo, pageNumber, pageSize);
            return new PaginatedResult<MechanicalComponentProductSearchResponseDto>
            {
                Items = result.Items.Select(mc =>
                        new MechanicalComponentProductSearchResponseDto(
                            mc.Id,
                            mc.Identifier,
                            mc.Name
                        )
                    )
                    .ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = result.TotalCount
            };
        }

        public async Task<MechanicalComponentInfoResponseDto> FindInfo(string componentId)
        {
            if (!Guid.TryParse(componentId, out _))
            {
                throw new InvalidCastException("Guid cannot be parsed");
            }
            Guid componentGuid = Guid.Parse(componentId);
            var component = await repository.FindById(componentGuid);
            if (component is null)
            {
                throw new NotFoundException("Component not found");
            }
            var quantity = await repository.CountQuantity(component);
            return new MechanicalComponentInfoResponseDto(
                component.Name,
                component.Identifier,
                quantity,
                component.Items.Select(ii => new MechanicalComponentInfoInvoiceResponseDto(
                    ii.Import.Id,
                    ii.Import.DateIssued,
                    new MechanicalComponentInfoProviderResponseDto(ii.Import.Provider.Id, ii.Import.Provider.Name, ii.Import.Provider.Address, ii.Import.Provider.PhoneNumber))
                ).ToList()
            );
        }

        public async Task<MechanicalComponentQuantitySumResponseDto> FindQuantitySum()
        {
            return new MechanicalComponentQuantitySumResponseDto(await repository.FindQuantitySum());
        }

        public async Task<MechanicalComponentTopFiveQuantityResponsesDto> FindTopFiveInQuantity()
        {
            var result = await repository.FindTopFiveInQuantity();
            var responses = new List<MechanicalComponentTopFiveQuantityResponseDto>();
            foreach (var r in result)
            {
                var quantity = await repository.CountQuantity(r);
                responses.Add(new MechanicalComponentTopFiveQuantityResponseDto(r.Id, r.Name, r.Identifier, quantity));
            }
            return new MechanicalComponentTopFiveQuantityResponsesDto(responses);
        }
    }
}
