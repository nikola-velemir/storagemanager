using StoreManager.Application.Common;

namespace StoreManager.Application.BusinessPartner.Base.Errors;

public static class BusinessPartnerErrors
{
    public static Error InvalidCoordinatesError => new Error("InvalidCoordinates", 400,"Invalid coordinates");
    public static Error InvalidBusinessPartnerTypeError => new Error("InvalidBusinessPartnerType", 400, "Invalid business partner type");
    public static Error PartnerIdParseError => new Error("PartnerIdParseError", 500, "Invalid business partner id");
    public static Error PartnerNotFoundError => new Error("PartnerNotFound", 404, "Business partner not found");
}