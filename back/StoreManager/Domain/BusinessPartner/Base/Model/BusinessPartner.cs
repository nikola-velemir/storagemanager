using StoreManager.Domain.BusinessPartner.Shared;

namespace StoreManager.Domain.BusinessPartner.Base.Model;

public class BusinessPartner
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required Address Address { get; set; }
    public required string PhoneNumber { get; set; }
    public required BusinessPartnerType Type { get; set; }
}