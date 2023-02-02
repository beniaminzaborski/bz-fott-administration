namespace Bz.Fott.Administration.Application.Competitions;

public class CompetitionDto
{
    public Guid Id { get; set; }
    public DateTime StartAt { get; set; }
    public DistanceDto Distance { get; set; } = null!;
    public CompetitionPlaceDto Place { get; set; } = null!;
    public int MaxCompetitors { get; set; }
    public string Status { get; set; } = null!;

    public IEnumerable<CheckpointDto> Checkpoints { get; set; } = new List<CheckpointDto>();
}
