using StoreManager.Application.GeoCoding;

namespace StoreManager.Infrastructure.GeoCoding;

public class LocationIqService : IGeoCodingService
{
    private static readonly string key = "pk.3b1f2511dd3a5d61b3abdef60a419901";

    private readonly string baseUrl =
        $"https://us1.locationiq.com/v1/search?key={key}&q=";

    private static readonly string format = "json&";

    public LocationIqService()
    {
        
    }

    public async Task<string> ForwardGeoCode(string city, string street, string streetNumber)
    {
        using var client = new HttpClient();
        var parsedCity = city.Replace(" ", "%20");
        var parsedStreet = street.Replace(" ", "%20");
        var parsedStreetNumber = streetNumber.Replace(" ", "%20");
        var url = $"{baseUrl}{parsedStreetNumber}%2C{parsedStreet}%2C{parsedCity}&format={format}";

        try
        {
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (HttpRequestException e)
        {
            throw new BadHttpRequestException("A");
        }
    }
}