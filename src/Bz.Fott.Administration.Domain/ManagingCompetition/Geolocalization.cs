namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public record Geolocalization
{
    public Geolocalization(decimal latitude, decimal longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public decimal Latitude { get; init; }
    public decimal Longitude { get; init; }
}
