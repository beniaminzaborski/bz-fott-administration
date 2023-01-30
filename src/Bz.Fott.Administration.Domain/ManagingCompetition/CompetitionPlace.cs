namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public record CompetitionPlace
{
    public CompetitionPlace(string city, int longitute, int latitude)
    {
        City = city;
        Longitute = longitute;
        Latitude = latitude;
    }

    public string City { get; init; }
    public int Longitute { get; init; }
    public int Latitude { get; init; }
}
