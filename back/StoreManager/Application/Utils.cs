using StoreManager.Domain.BusinessPartner.Shared;

namespace StoreManager.Application;

public static class Utils
{
    public static string FormatAddress(Address address)
    {
        return address.City + ", " + address.Street + " " + address.StreetNumber;
    }
}