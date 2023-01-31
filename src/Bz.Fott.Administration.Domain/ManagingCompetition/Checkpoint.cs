using Bz.Fott.Administration.Domain.Common;

namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public class Checkpoint : Entity<Guid>
{
    private Checkpoint() { }

    public Checkpoint(Distance trackPoint)
	{
        TrackPoint = trackPoint;
	}

    public Distance TrackPoint { get; init; }
}
