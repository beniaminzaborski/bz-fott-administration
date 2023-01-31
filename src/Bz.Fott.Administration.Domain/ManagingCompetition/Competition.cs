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

        AddCheckpoint(CreateStartLineCheckpoint());
        AddCheckpoint(CreateFinishLineCheckpoint());
    }

    public Distance Distance { get; private set; }

    public CompetitionPlace Place { get; private set; }

    public DateTime StartAt { get; private set; }

    public int MaxCompetitors { get; private set; }

    public CompetitionStatus Status { get; private set; }

    private IList<Checkpoint> _checkpoints = new List<Checkpoint>();
    public IReadOnlyCollection<Checkpoint> Checkpoints => _checkpoints.OrderBy(c => c.TrackPoint.Amount).ToList().AsReadOnly();

    public void AddCheckpoint(Checkpoint checkpoint)
    { 
        _checkpoints.Add(checkpoint);
    }

    public void RemoveCheckpoint(Checkpoint checkpoint)
    { 
        _checkpoints.Remove(checkpoint);
    }

    public void OpenRegistration()
    {
        if (Status != CompetitionStatus.Draft) return;

        Status = CompetitionStatus.OpenedForRegistration;
        QueueDomainEvent(new CompetitionOpenedForRegistration());
    }

    public void CompleteRegistration() 
    {
        if (Status != CompetitionStatus.OpenedForRegistration) return;

        Status = CompetitionStatus.RegistrationCompleted;
        QueueDomainEvent(new CompetitionRegistrationCompleted());
    }

    public void ChangeMaxCompetitors(int maxCompetitors)
    {
        if ((Status == CompetitionStatus.Draft
            || Status == CompetitionStatus.OpenedForRegistration)
            && maxCompetitors > MaxCompetitors)
        { 
            MaxCompetitors = maxCompetitors;
            QueueDomainEvent(new CompetitionMaxCompetitorsIncreased());
        }
        else if (Status == CompetitionStatus.Draft
            && maxCompetitors < MaxCompetitors)
        {
            MaxCompetitors = maxCompetitors;
            QueueDomainEvent(new CompetitionMaxCompetitorsDecreased());
        }
        else
            throw new CompetitionMaxCompetitorsChangeNotAllowedException();
    }

    private Checkpoint CreateStartLineCheckpoint()
    {
        return new Checkpoint(new Distance(0, Distance.Unit));
    }

    private Checkpoint CreateFinishLineCheckpoint()
    {
        return new Checkpoint(new Distance(Distance.Amount, Distance.Unit));
    }
}
