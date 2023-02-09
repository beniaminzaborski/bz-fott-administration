namespace Bz.Fott.Administration.Application.Competitions;

public record CreateCompetitionDto(
    DateTime StartAt,
    DistanceDto Distance,
    CompetitionPlaceDto Place,
    int MaxCompetitors)
{
}
