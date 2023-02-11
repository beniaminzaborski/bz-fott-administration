namespace Bz.Fott.Administration.Application.Competitions;

public record CompetitionPlaceDto
{
    public string City { get; init; }
    public decimal Latitude { get; init; }
    public decimal Longitute { get; init; }
}
