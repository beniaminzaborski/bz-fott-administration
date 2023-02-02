namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public record CompetitionPlace
{
    private CompetitionPlace() { }

    public CompetitionPlace(string city, Geolocalization localization)
    {
        City = city;
        Localization = localization;
    }

    public string City { get; init; }
    public Geolocalization Localization { get; init; }
}
