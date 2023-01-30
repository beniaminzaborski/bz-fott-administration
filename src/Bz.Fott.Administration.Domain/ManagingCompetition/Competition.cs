using Bz.Fott.Administration.Domain.Common;

namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public class Competition : Entity<CompetitionId>, IAggregateRoot
{
    private Competition() { }

    public Competition(
        CompetitionId id,
        Distance dictance,
        DateTime startAt,
        int maxCompetitors,
        CompetitionPlace place)
    {
        if (maxCompetitors <= 0) throw new ArgumentException("Numbers of maximum competitors must be greater than 0", nameof(maxCompetitors));

        Id = id;
        Distance = dictance;
        StartAt = startAt;
        MaxCompetitors = maxCompetitors;
        Place = place;
        Status = CompetitionStatus.Draft;
    }

    public Distance Distance { get; private set; }

    public CompetitionPlace Place { get; private set; }

    public DateTime StartAt { get; private set; }

    public int MaxCompetitors { get; private set; }

    public CompetitionStatus Status { get; private set; }

    public void Approve()
    {
        Status = CompetitionStatus.Approved;

        QueueDomainEvent(new CompetitionApproved());
    }
}
