namespace StoreManager.Infrastructure.BusinessPartner.Base;

public class BusinessPartnerModel
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public required BusinessPartnerType Type { get; set; }
}