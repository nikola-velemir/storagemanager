using StoreManager.Application.MechanicalComponent.DTO.Find;
using StoreManager.Application.MechanicalComponent.DTO.Info;
using StoreManager.Application.MechanicalComponent.DTO.Quantity;
using StoreManager.Application.MechanicalComponent.DTO.Search;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Application.Shared;
using StoreManager.Infrastructure.MechanicalComponent.Service;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.MechanicalComponent.Service
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
            var result = await repository.FindByInvoiceIdAsync(invoiceGuid);

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

        public async Task<PaginatedResult<MechanicalComponentSearchResponseDto>> FindFiltered(string? providerId,
            string? componentInfo, int pageNumber, int pageSize)
        {
            Guid? id = null;
            if (Guid.TryParse(providerId, out var tempId))
            {
                id = tempId;
            }

            var result = await repository.FindFilteredAsync(id, componentInfo, pageNumber, pageSize);
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
                                        Utils.FormatAddress(ii.Import.Provider.Address),
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

        public async Task<PaginatedResult<MechanicalComponentProductSearchResponseDto>> FindFilteredForProduct(
            string? providerId, string? componentInfo, int pageNumber, int pageSize)
        {
            Guid? id = null;
            if (Guid.TryParse(providerId, out var tempId))
            {
                id = tempId;
            }

            var result = await repository.FindFilteredForProductAsync(id, componentInfo, pageNumber, pageSize);
            return new PaginatedResult<MechanicalComponentProductSearchResponseDto>
            {
                Items = result.Items.Select(mc =>
                        new MechanicalComponentProductSearchResponseDto(
                            mc.Id,
                            mc.Identifier,
                            mc.Name,
                            mc.CurrentStock
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
                    new MechanicalComponentInfoProviderResponseDto(ii.Import.Provider.Id, ii.Import.Provider.Name,
                        Utils.FormatAddress(ii.Import.Provider.Address), ii.Import.Provider.PhoneNumber))
                ).ToList()
            );
        }

        public async Task<MechanicalComponentQuantitySumResponseDto> FindQuantitySum()
        {
            return new MechanicalComponentQuantitySumResponseDto(await repository.FindQuantitySumAsync());
        }

        public async Task<MechanicalComponentTopFiveQuantityResponsesDto> FindTopFiveInQuantity()
        {
            var result = await repository.FindTopFiveInQuantityAsync();
            var responses = new List<MechanicalComponentTopFiveQuantityResponseDto>();
            foreach (var r in result)
            {
                var quantity = await repository.CountQuantityAsync(r);
                responses.Add(new MechanicalComponentTopFiveQuantityResponseDto(r.Id, r.Name, r.Identifier, quantity));
            }

            return new MechanicalComponentTopFiveQuantityResponsesDto(responses);
        }
    }
}