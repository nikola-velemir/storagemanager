using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Search
{
    public record class MechanicalComponentSearchResponsesDTO(PaginatedResult<MechanicalComponentSearchResponseDTO> responses);
}
