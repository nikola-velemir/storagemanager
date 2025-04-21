namespace StoreManager.Application.BusinessPartner.Base.DTO;

public sealed record CreateBusinessPartnerRequest(string Name, string PhoneNumber, string Role, string City, string Street, string StreetNumber);