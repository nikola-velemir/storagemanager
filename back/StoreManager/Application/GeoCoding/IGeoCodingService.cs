namespace StoreManager.Application.GeoCoding;

public interface IGeoCodingService
{
    Task<string> ForwardGeoCode(string city, string street, string streetNumber);
}