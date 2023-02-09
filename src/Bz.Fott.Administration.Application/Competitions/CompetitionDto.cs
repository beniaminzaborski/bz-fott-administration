namespace Bz.Fott.Administration.Application.Competitions;

public record CompetitionDto(
    Guid Id,
    DateTime StartAt,
    DistanceDto Distance,
    CompetitionPlaceDto Place,
    int MaxCompetitors,
    string Status,
    IEnumerable<CheckpointDto> Checkpoints)
{
}
