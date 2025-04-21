namespace StoreManager.Application.BusinessPartner.Base.DTO;

public sealed record BusinessPartnerAddressResponseDto(string City, string Street, string StreetNumber, double Latitude, double Longitude);