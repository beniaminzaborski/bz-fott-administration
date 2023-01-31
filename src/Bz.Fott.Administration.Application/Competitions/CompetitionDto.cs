namespace Bz.Fott.Administration.Application.Competitions;

public class CompetitionDto
{
    public Guid Id { get; set; }
    public DateTime StartAt { get; set; }
    public string City { get; set; }
    public decimal DistanceAmount { get; internal set; }
    public string DistanceUnit { get; internal set; }
    public int Latitude { get; internal set; }
    public int Longitute { get; internal set; }
    public int MaxCompetitors { get; internal set; }
    public string Status { get; internal set; }
}
