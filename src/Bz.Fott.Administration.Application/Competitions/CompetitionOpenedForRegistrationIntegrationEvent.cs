using Bz.Fott.Administration.Domain.ManagingCompetition;

namespace Bz.Fott.Administration.Application.Competitions;

public sealed record CompetitionOpenedForRegistrationIntegrationEvent(
    Guid Id,
    CompetitionPlaceDto Place,
    DistanceDto Distance,
    DateTime StartAt,
    int MaxCompetitors,
    IEnumerable<CheckpointDto> Checkpoints) { }
