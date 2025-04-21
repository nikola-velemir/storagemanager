using MediatR;
using Newtonsoft.Json;
using StoreManager.Application.BusinessPartner.Base.Command;
using StoreManager.Application.BusinessPartner.Base.Repository;
using StoreManager.Application.GeoCoding;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Exporter.Model;
using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Domain.BusinessPartner.Shared;

namespace StoreManager.Application.BusinessPartner.Base.Handler;

public class CreateBusinessPartnerCommandHandler(
    IBusinessPartnerRepository repository,
    IGeoCodingService geoCodingService)
    : IRequestHandler<CreateBusinessPartnerCommand>
{
    public async Task<Unit> Handle(CreateBusinessPartnerCommand request, CancellationToken cancellationToken)
    {
        var geocodingResponse =
            await geoCodingService.ForwardGeoCode(request.City, request.Street, request.StreetNumber);
        
        var coordinates = GetCoordinates(geocodingResponse);
        if (!Enum.TryParse<BusinessPartnerType>(request.Role, out _))
            throw new InvalidCastException("Could not cast to business partner");

        var role = Enum.Parse<BusinessPartnerType>(request.Role);

        var address = new Address(request.City, request.Street, request.StreetNumber, coordinates.Latitude,
            coordinates.Longitude);
        BusinessPartnerModel partner = role switch
        {
            BusinessPartnerType.Exporter => new ExporterModel
            {
                Address =address,
                Id = Guid.NewGuid(),
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Type = role
            },
            BusinessPartnerType.Provider => new ProviderModel
            {
                Address = address,
                Id = Guid.NewGuid(),
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Type = role
            },
            _ => throw new ArgumentException()
        };

        await repository.CreateAsync(partner);
        return Unit.Value;
    }

    private static (double Latitude, double Longitude) GetCoordinates(string responseString)
    {
        var responseDictionary = (JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(responseString) ??
                                  throw new InvalidOperationException()).First();

        if (!responseDictionary.TryGetValue("lat", out var lat) || !responseDictionary.TryGetValue("lon", out var lon))
            throw new InvalidOperationException();
        var parsedLatitude = ((string)lat).Replace('.',',');
        var parsedLongitude = ((string)lon).Replace('.',',');
        return (double.Parse(parsedLatitude), double.Parse(parsedLongitude));
    }
}