using Bz.Fott.Administration.Domain.Common;

namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public sealed record CompetitionMaxCompetitorsIncreased(
    CompetitionId Id,
    int MaxCompetitors)
    : IDomainEvent { }
