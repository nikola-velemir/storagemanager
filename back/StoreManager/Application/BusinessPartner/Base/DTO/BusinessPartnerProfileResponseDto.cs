namespace StoreManager.Application.BusinessPartner.Base.DTO;

public sealed record BusinessPartnerProfileResponseDto(
    string Name,
    BusinessPartnerAddressResponseDto Address,
    string PartnerType,
    string PhoneNumber);