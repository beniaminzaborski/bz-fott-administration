namespace Bz.Fott.Administration.Application.Competitions;

public class CompetitionDto
{
    public Guid Id { get; set; }
    public DateTime StartAt { get; set; }
    public string City { get; set; }
    public decimal DistanceAmount { get; set; }
    public string DistanceUnit { get; set; }
    public int Latitude { get; set; }
    public int Longitute { get; set; }
    public int MaxCompetitors { get; set; }
    public string Status { get; set; }

    public IEnumerable<CheckpointDto> Checkpoints { get; set; }
}
