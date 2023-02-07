namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public class GeolocalizationLatitudeInvalidException : Exception
{
    public GeolocalizationLatitudeInvalidException() { }

    public GeolocalizationLatitudeInvalidException(string message) : base(message) { }

    public GeolocalizationLatitudeInvalidException(string message, Exception inner) : base(message, inner) { }
}
