
using StoreManager.Infrastructure.MechanicalComponent.DTO;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Find;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Info;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Search;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.MechanicalComponent.Service
{
    public class MechanicalComponentService : IMechanicalComponentService
    {
        private readonly IMechanicalComponentRepository _repository;
        public MechanicalComponentService(IMechanicalComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<MechanicalComponentFindResponsesDTO> FindByInvoiceId(string invoiceId)
        {
            if (!Guid.TryParse(invoiceId, out _))
            {
                throw new InvalidCastException("Guid cannot be parsed");
            }
            Guid invoiceGuid = Guid.Parse(invoiceId);
            var result = await _repository.FindByInvoiceId(invoiceGuid);

            return new MechanicalComponentFindResponsesDTO(
                result.Select(mc =>
                new MechanicalComponentFindResponseDTO(
                    mc.Id,
                    mc.Identifier,
                    mc.Name,
                    mc.Items.First(ii => ii.InvoiceId.Equals(invoiceGuid)).Quantity,
                    mc.Items.First(ii => ii.InvoiceId.Equals(invoiceGuid)).PricePerPiece
                    )
                ).ToList());
        }

        public async Task<PaginatedResult<MechanicalComponentSearchResponseDTO>> FindFiltered(string? providerId, string? componentInfo, int pageNumber, int pageSize)
        {
            Guid? id = null;
            if (Guid.TryParse(providerId, out var tempId))
            {
                id = tempId;
            }

            var result = await _repository.FindFiltered(id, componentInfo, pageNumber, pageSize);
            return new PaginatedResult<MechanicalComponentSearchResponseDTO>
            {
                Items = result.Items.Select(mc =>
                new MechanicalComponentSearchResponseDTO(
                    mc.Id,
                    mc.Identifier,
                    mc.Name,
                    mc.Items.Select(ii =>
                    new MechanicalComponentSearchInvoiceResponseDTO(
                        ii.Invoice.Id,
                        ii.Invoice.DateIssued,
                       new MechanicalComponentSearchProviderResponseDTO(
                           ii.Invoice.Provider.Id,
                           ii.Invoice.Provider.Name,
                           ii.Invoice.Provider.Adress,
                           ii.Invoice.Provider.PhoneNumber
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

        public async Task<MechanicalComponentInfoResponseDTO> FindInfo(string componentId)
        {
            if (!Guid.TryParse(componentId, out _))
            {
                throw new InvalidCastException("Guid cannot be parsed");
            }
            Guid componentGuid = Guid.Parse(componentId);
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
