namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public record CompetitionPlace
{
    private CompetitionPlace() { }

    public CompetitionPlace(string city, Geolocalization localization)
    {
        if (string.IsNullOrEmpty(city) || city.Length > 100) throw new CompetitionPlaceCityInvalidException();

        City = city;
        Localization = localization;
    }

    public string City { get; init; }
    public Geolocalization Localization { get; init; }
}
