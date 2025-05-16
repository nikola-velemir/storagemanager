namespace StoreManager.Application.Invoice.Base.Error;

public static class InvoiceErrors
{
    public static Common.Error InvoiceIdParseError => new Common.Error("InvoiceIdParseError",400,"Failed to parse invoice id");
    public static Common.Error InvoiceNotFound => new Common.Error("InvoiceNotFound",404,"Invoice Not Found");
    public static Common.Error QuantityInsufficient = new Common.Error("QuantityInsufficient",500,"Quantity Infsuficient");

}