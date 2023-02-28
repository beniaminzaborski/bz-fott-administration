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
          new CompetitionPlace("Kielce", new Geolocalization(50.86022655378784m, 20.623838070358033m)));

        // Assert
        Assert.Equal(CompetitionStatus.Draft, competition.Status);
    }

    [Fact]
    public void Create_ShouldHaveStartAndFinishCheckpoint()
    {
        // Arrange & Act
        var competition = new Competition(
          CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
          DistanceHelper.Marathon(),
          new DateTime(2032, 02, 08, 10, 00, 00),
          8000,
          new CompetitionPlace("Kielce", new Geolocalization(50.86022655378784m, 20.623838070358033m)));

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
           new CompetitionPlace("Kielce", new Geolocalization(50.86022655378784m, 20.623838070358033m)));

        // Act
        competition.OpenRegistration();

        // Assert
        Assert.Equal(CompetitionStatus.OpenedForRegistration, competition.Status);
        Assert.Contains(competition.GetDomainEvents(), e => e is CompetitionOpenedForRegistration);
    }

    [Fact]
    public void OpenRegistration_ShouldThrowCannotOpenRegistrationException_IfCompetitionIsInOpenedForRegistrationState()
    {
        // Arrange
        var competition = new Competition(
           CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
           DistanceHelper.Marathon(),
           new DateTime(2032, 02, 08, 10, 00, 00),
           8000,
           new CompetitionPlace("Kielce", new Geolocalization(50.86022655378784m, 20.623838070358033m)));
        competition.OpenRegistration();

        // Act
        Action action = () => competition.OpenRegistration();

        // Assert
        var ex = Assert.Throws<CannotOpenRegistrationException>(action);
    }

    [Fact]
    public void OpenRegistration_ShouldThrowCannotOpenRegistrationException_IfCompetitionIsInRegistrationCompletedState()
    {
        // Arrange
        var competition = new Competition(
           CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
           DistanceHelper.Marathon(),
           new DateTime(2032, 02, 08, 10, 00, 00),
           8000,
           new CompetitionPlace("Kielce", new Geolocalization(50.86022655378784m, 20.623838070358033m)));
        competition.OpenRegistration();
        competition.CompleteRegistration();

        // Act
        Action action = () => competition.OpenRegistration();

        // Assert
        var ex = Assert.Throws<CannotOpenRegistrationException>(action);
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
           new CompetitionPlace("Kielce", new Geolocalization(50.86022655378784m, 20.623838070358033m)));

        // Act
        competition.ChangeMaxCompetitors(10000);

        // Assert
        Assert.Equal(CompetitionStatus.Draft, competition.Status);
        Assert.Equal(10000, competition.MaxCompetitors);
        Assert.Contains(competition.GetDomainEvents(), e => e is CompetitionMaxCompetitorsIncreased);
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
           new CompetitionPlace("Kielce", new Geolocalization(50.86022655378784m, 20.623838070358033m)));

        // Act
        competition.ChangeMaxCompetitors(7000);

        // Assert
        Assert.Equal(CompetitionStatus.Draft, competition.Status);
        Assert.Equal(7000, competition.MaxCompetitors);
        Assert.Contains(competition.GetDomainEvents(), e => e is CompetitionMaxCompetitorsDecreased);
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
           new CompetitionPlace("Kielce", new Geolocalization(50.86022655378784m, 20.623838070358033m)));
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
           new CompetitionPlace("Kielce", new Geolocalization(50.86022655378784m, 20.623838070358033m)));
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
           new CompetitionPlace("Kielce", new Geolocalization(50.86022655378784m, 20.623838070358033m)));
        competition.OpenRegistration();

        // Act
        competition.CompleteRegistration();

        // Assert
        Assert.Equal(CompetitionStatus.RegistrationCompleted, competition.Status);
        Assert.Contains(competition.GetDomainEvents(), e => e is CompetitionRegistrationCompleted);
    }

    [Fact]
    public void CompleteRegistration_ShouldThrowCannotCompleteRegistrationException_IfCompetitionIsInDraftState()
    {
        // Arrange
        var competition = new Competition(
           CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
           DistanceHelper.Marathon(),
           new DateTime(2032, 02, 08, 10, 00, 00),
           8000,
           new CompetitionPlace("Kielce", new Geolocalization(50.86022655378784m, 20.623838070358033m)));

        // Act
        Action action = () => competition.CompleteRegistration();

        // Assert
        var ex = Assert.Throws<CannotCompleteRegistrationException>(action);
    }

    [Fact]
    public void CompleteRegistration_ShouldThrowCannotCompleteRegistrationException_IfCompetitionIsInRegistrationCompletedState()
    {
        // Arrange
        var competition = new Competition(
           CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
           DistanceHelper.Marathon(),
           new DateTime(2032, 02, 08, 10, 00, 00),
           8000,
           new CompetitionPlace("Kielce", new Geolocalization(50.86022655378784m, 20.623838070358033m)));
        competition.OpenRegistration();
        competition.CompleteRegistration();

        // Act
        Action action = () => competition.CompleteRegistration();

        // Assert
        var ex = Assert.Throws<CannotCompleteRegistrationException>(action);
    }

    [Fact]
    public void AddCheckpoint_ShouldThrowCheckpointAlreadyExistsException_IfCheckpointWithTheSameValuesAlreadyExists()
    {
        // Arrange
        var competition = new Competition(
           CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
           DistanceHelper.Marathon(),
           new DateTime(2032, 02, 08, 10, 00, 00),
           8000,
           new CompetitionPlace("Kielce", new Geolocalization(50.86022655378784m, 20.623838070358033m)));

        // Act
        Action action = () => competition.AddCheckpoint(new Checkpoint(CheckpointId.From(Guid.NewGuid()), competition.Id, new Distance(0, DistanceUnit.Kilometers)));

        // Assert
        var ex = Assert.Throws<CheckpointAlreadyExistsException>(action);
    }

    [Fact]
    public void RemoveCheckpoint_ShouldThrowCheckpointNotExistsException_IfCheckpointWithTheIdNotExists()
    {
        // Arrange
        var competition = new Competition(
           CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
           DistanceHelper.Marathon(),
           new DateTime(2032, 02, 08, 10, 00, 00),
           8000,
           new CompetitionPlace("Kielce", new Geolocalization(50.86022655378784m, 20.623838070358033m)));

        // Act
        Action action = () => competition.RemoveCheckpoint(CheckpointId.From(Guid.NewGuid()));

        // Assert
        var ex = Assert.Throws<CheckpointNotExistsException>(action);
    }
}
