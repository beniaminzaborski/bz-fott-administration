namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public record Geolocalization
{
    public Geolocalization(decimal latitude, decimal longitude)
    {
        if (latitude < -90 || latitude > 90) throw new GeolocalizationLatitudeInvalidException();
        if (longitude < -180 || longitude > 180) throw new GeolocalizationLongitudeInvalidException();

        Latitude = latitude;
        Longitude = longitude;
    }

    public decimal Latitude { get; init; }
    public decimal Longitude { get; init; }
}
