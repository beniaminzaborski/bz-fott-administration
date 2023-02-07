namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public class GeolocalizationLongitudeInvalidException : Exception
{
    public GeolocalizationLongitudeInvalidException() { }

    public GeolocalizationLongitudeInvalidException(string message) : base(message) { }

    public GeolocalizationLongitudeInvalidException(string message, Exception inner) : base(message, inner) { }
}
