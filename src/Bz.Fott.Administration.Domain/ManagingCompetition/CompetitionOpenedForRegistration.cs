using Bz.Fott.Administration.Domain.Common;

namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public sealed record CompetitionOpenedForRegistration(
    CompetitionId Id,
    CompetitionPlace Place,
     Distance Distance,
     DateTime StartAt,
     int MaxCompetitors,
     IEnumerable<Checkpoint> Checkpoints)
    : IDomainEvent { }
