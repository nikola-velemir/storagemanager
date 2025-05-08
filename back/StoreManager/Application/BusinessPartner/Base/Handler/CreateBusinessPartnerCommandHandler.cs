using MediatR;
using Newtonsoft.Json;
using StoreManager.Application.BusinessPartner.Base.Command;
using StoreManager.Application.BusinessPartner.Base.Errors;
using StoreManager.Application.BusinessPartner.Base.Repository;
using StoreManager.Application.Common;
using StoreManager.Application.GeoCoding;
using StoreManager.Domain;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Exporter.Model;
using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Domain.BusinessPartner.Shared;

namespace StoreManager.Application.BusinessPartner.Base.Handler;

public class CreateBusinessPartnerCommandHandler(
    IUnitOfWork unitOfWork,
    IBusinessPartnerRepository repository,
    IGeoCodingService geoCodingService)
    : IRequestHandler<CreateBusinessPartnerCommand, Result>
{
    public async Task<Result> Handle(CreateBusinessPartnerCommand request, CancellationToken cancellationToken)
    {
        var geocodingResponse =
            await geoCodingService.ForwardGeoCode(request.City, request.Street, request.StreetNumber);
        (double Latitude, double Longitude)? coordinates = null;
        try
        {
            coordinates = GetCoordinates(geocodingResponse);
        }
        catch (Exception)
        {
            return BusinessPartnerErrors.InvalidCoordinatesError;
        }

        if (!Enum.TryParse<BusinessPartnerType>(request.Role, out _))
            return BusinessPartnerErrors.InvalidBusinessPartnerTypeError;

        var role = Enum.Parse<BusinessPartnerType>(request.Role);

        var address = new Address(request.City, request.Street, request.StreetNumber, coordinates.Value.Latitude,
            coordinates.Value.Longitude);
        Domain.BusinessPartner.Base.Model.BusinessPartner partner = role switch
        {
            BusinessPartnerType.Exporter => new Domain.BusinessPartner.Exporter.Model.Exporter
            {
                Address = address,
                Id = Guid.NewGuid(),
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Type = role
            },
            BusinessPartnerType.Provider => new Domain.BusinessPartner.Provider.Model.Provider
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
        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }

    private static (double Latitude, double Longitude) GetCoordinates(string responseString)
    {
        var responseDictionary = (JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(responseString) ??
                                  throw new InvalidOperationException()).First();

        if (!responseDictionary.TryGetValue("lat", out var lat) || !responseDictionary.TryGetValue("lon", out var lon))
            throw new InvalidOperationException();
        var parsedLatitude = ((string)lat).Replace('.', ',');
        var parsedLongitude = ((string)lon).Replace('.', ',');
        return (double.Parse(parsedLatitude), double.Parse(parsedLongitude));
    }
}