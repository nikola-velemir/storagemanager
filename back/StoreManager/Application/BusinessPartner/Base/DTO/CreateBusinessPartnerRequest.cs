namespace StoreManager.Application.BusinessPartner.Base.DTO;

public sealed record CreateBusinessPartnerRequest(string Name, string Address, string PhoneNumber, string Role);