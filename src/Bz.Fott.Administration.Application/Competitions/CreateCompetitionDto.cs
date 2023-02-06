namespace Bz.Fott.Administration.Application.Competitions;

public class CreateCompetitionDto
{
    public DateTime StartAt { get; set; }
    public DistanceDto Distance { get; set; } = null!;
    public CompetitionPlaceDto Place { get; set; } = null!;
    public int MaxCompetitors { get; set; }
}
