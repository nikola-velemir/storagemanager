namespace StoreManager.Application.MechanicalComponent.DTO.Quantity
{
    public sealed record MechanicalComponentTopFiveQuantityResponsesDto(
        List<MechanicalComponentTopFiveQuantityResponseDto> Components);
}