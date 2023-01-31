using Bz.Fott.Administration.Domain.ManagingCompetition;
using Bz.Fott.Administration.Domain.Utils;

namespace Bz.Fott.Administration.Domain.UnitTests.ManagingCompetition;

public class CompetitionTests
{
    [Fact]
    public void Create_ShouldBeInDraftState()
    {
        // Arrange & Act
        var competition = new Competition(
          CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
          DistanceHelper.Marathon(),
          new DateTime(2032, 02, 08, 10, 00, 00),
          8000,
          new CompetitionPlace("Kielce", 123, 321));

        // Assert
        Assert.Equal(CompetitionStatus.Draft, competition.Status);
    }

    [Fact]
    public void Create_ShouldHasStartAndFinishCheckpoint()
    {
        // Arrange & Act
        var competition = new Competition(
          CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
          DistanceHelper.Marathon(),
          new DateTime(2032, 02, 08, 10, 00, 00),
          8000,
          new CompetitionPlace("Kielce", 123, 321));

        // Assert
        var checkpoints = competition.Checkpoints;
        Assert.Equal(2, checkpoints.Count);

        var firstCheckpoint = checkpoints.First();
        Assert.Equal(0, firstCheckpoint.TrackPoint.Amount);
        Assert.Equal(competition.Distance.Unit, firstCheckpoint.TrackPoint.Unit);

        var lastCheckpoint = checkpoints.Last();
        Assert.Equal(competition.Distance.Amount, lastCheckpoint.TrackPoint.Amount);
        Assert.Equal(competition.Distance.Unit, lastCheckpoint.TrackPoint.Unit);
    }

    [Fact]
    public void OpenRegistration_ShouldChangeStatusToOpenedForRegistrationAndRaiseCompetitionOpenedForRegistrationDomainEvent_IfCompetitionIsInDraftState()
    {
        // Arrange
        var competition = new Competition(
           CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
           DistanceHelper.Marathon(),
           new DateTime(2032, 02, 08, 10, 00, 00),
           8000,
           new CompetitionPlace("Kielce", 123, 321));

        // Act
        competition.OpenRegistration();

        // Assert
        Assert.Equal(CompetitionStatus.OpenedForRegistration, competition.Status);
        Assert.Equal(1, competition.GetDomainEvents().Count);
        Assert.IsType<CompetitionOpenedForRegistration>(competition.GetDomainEvents().First());
    }

    [Fact]
    public void ChangeMaxCompetitors_ShouldIncreaseMaxCompetitorsAndRaiseCompetitionMaxCompetitorsIncreasedDomainEvent_IfCompetitionIsInDraftState()
    {
        // Arrange
        var competition = new Competition(
           CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
           DistanceHelper.Marathon(),
           new DateTime(2032, 02, 08, 10, 00, 00),
           8000,
           new CompetitionPlace("Kielce", 123, 321));

        // Act
        competition.ChangeMaxCompetitors(10000);

        // Assert
        Assert.Equal(CompetitionStatus.Draft, competition.Status);
        Assert.Equal(10000, competition.MaxCompetitors);
        Assert.Equal(1, competition.GetDomainEvents().Count);
        Assert.IsType<CompetitionMaxCompetitorsIncreased>(competition.GetDomainEvents().First());
    }

    [Fact]
    public void ChangeMaxCompetitors_ShouldDecreaseMaxCompetitorsAndRaiseCompetitionMaxCompetitorsDecreasedDomainEvent_IfCompetitionIsInDraftState()
    {
        // Arrange
        var competition = new Competition(
           CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
           DistanceHelper.Marathon(),
           new DateTime(2032, 02, 08, 10, 00, 00),
           8000,
           new CompetitionPlace("Kielce", 123, 321));

        // Act
        competition.ChangeMaxCompetitors(7000);

        // Assert
        Assert.Equal(CompetitionStatus.Draft, competition.Status);
        Assert.Equal(7000, competition.MaxCompetitors);
        Assert.Equal(1, competition.GetDomainEvents().Count);
        Assert.IsType<CompetitionMaxCompetitorsDecreased>(competition.GetDomainEvents().First());
    }

    [Fact]
    public void ChangeMaxCompetitors_ShouldIncreaseMaxCompetitorsAndRaiseCompetitionMaxCompetitorsIncreasedDomainEvent_IfCompetitionIsInOpenedToRegistrationState()
    {
        // Arrange
        var competition = new Competition(
           CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
           DistanceHelper.Marathon(),
           new DateTime(2032, 02, 08, 10, 00, 00),
           8000,
           new CompetitionPlace("Kielce", 123, 321));
        competition.OpenRegistration();

        // Act
        competition.ChangeMaxCompetitors(10000);

        // Assert
        Assert.Equal(CompetitionStatus.OpenedForRegistration, competition.Status);
        Assert.Equal(10000, competition.MaxCompetitors);
        Assert.Contains(competition.GetDomainEvents(), e => e is CompetitionMaxCompetitorsIncreased);
    }

    [Fact]
    public void ChangeMaxCompetitors_ShouldThrowCompetitionMaxCompetitorsChangeNotAllowedException_IfDecreaseMaxCompetitorsAndCompetitionIsInOpenedToRegistrationState()
    {
        // Arrange
        var competition = new Competition(
           CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
           DistanceHelper.Marathon(),
           new DateTime(2032, 02, 08, 10, 00, 00),
           8000,
           new CompetitionPlace("Kielce", 123, 321));
        competition.OpenRegistration();

        // Act
        Action action = () => competition.ChangeMaxCompetitors(7000);

        // Assert
        var ex = Assert.Throws<CompetitionMaxCompetitorsChangeNotAllowedException>(action);
        Assert.DoesNotContain(competition.GetDomainEvents(), e => e is CompetitionMaxCompetitorsDecreased);
    }

    [Fact]
    public void CompleteRegistration_ShouldChangeStatusToRegistrationCompletedAndRaiseCompetitionRegistrationCompletedDomainEvent_IfCompetitionIsInOpenedToRegistrationState()
    {
        // Arrange
        var competition = new Competition(
           CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
           DistanceHelper.Marathon(),
           new DateTime(2032, 02, 08, 10, 00, 00),
           8000,
           new CompetitionPlace("Kielce", 123, 321));
        competition.OpenRegistration();

        // Act
        competition.CompleteRegistration();

        // Assert
        Assert.Equal(CompetitionStatus.RegistrationCompleted, competition.Status);
        Assert.Contains(competition.GetDomainEvents(), e => e is CompetitionRegistrationCompleted);
    }
}
