using Bz.Fott.Administration.Domain.Common;

namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public class Checkpoint : Entity<CheckpointId>
{
    private Checkpoint() { }

    public Checkpoint(CheckpointId id, CompetitionId competitionId, Distance trackPoint)
	{
        Id = id;
        CompetitionId = competitionId;
        TrackPoint = trackPoint;
	}

    public CompetitionId CompetitionId { get; init; }

    public Distance TrackPoint { get; init; }
}
