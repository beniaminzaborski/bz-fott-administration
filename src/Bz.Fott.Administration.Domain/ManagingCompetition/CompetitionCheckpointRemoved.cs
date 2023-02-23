using Bz.Fott.Administration.Domain.Common;

namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public sealed record CompetitionCheckpointRemoved(
    CompetitionId CompetitionId,
    CheckpointId CheckpointId,
    Distance TrackPoint) : IDomainEvent { }
