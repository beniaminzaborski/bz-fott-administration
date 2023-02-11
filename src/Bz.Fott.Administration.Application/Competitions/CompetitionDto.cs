namespace Bz.Fott.Administration.Application.Competitions;

public record CompetitionDto
{
    public Guid Id { get; init; }
    public DateTime StartAt { get; init; }
    public DistanceDto Distance { get; init; }
    public CompetitionPlaceDto Place { get; init; }
    public int MaxCompetitors { get; init; }
    public string Status { get; init; }
    public IEnumerable<CheckpointDto> Checkpoints { get; init; }
}
