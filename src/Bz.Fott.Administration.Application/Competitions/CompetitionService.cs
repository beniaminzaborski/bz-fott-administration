using Bz.Fott.Administration.Application.Common;
using Bz.Fott.Administration.Application.Common.Exceptions;
using Bz.Fott.Administration.Domain.ManagingCompetition;
using Bz.Fott.Administration.Domain.Utils;

namespace Bz.Fott.Administration.Application.Competitions;

internal class CompetitionService : ICompetitionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompetitionRepository _competitionRepository;

    public CompetitionService(
        IUnitOfWork unitOfWork,
        ICompetitionRepository competitionRepository)
    {
        _unitOfWork = unitOfWork;
        _competitionRepository = competitionRepository;
    }

    public async Task<Guid> CreateCompetitionAsync(CreateCompetitionDto dto)
    {
        // TODO: Validate DTO

        // TODO: Map DTO to Entity
        var competition = new Competition(
            CompetitionId.From(Guid.NewGuid()),
            DistanceHelper.From(dto.Distance.Amount, dto.Distance.Unit),
            dto.StartAt,
            dto.MaxCompetitors,
            new CompetitionPlace(dto.Place.City, new Geolocalization(dto.Place.Latitude, dto.Place.Longitute)));

        await _competitionRepository.CreateAsync(competition);
        await _unitOfWork.CommitAsync();

        return competition.Id.Value;
    }

    public async Task<CompetitionDto> GetCompetitionAsync(Guid id)
    {
        var competition = await _competitionRepository.GetAsync(CompetitionId.From(id), i => i.Checkpoints);
        if (competition is null) throw new NotFoundException();
        return new CompetitionDto
        {
            Id = competition.Id.Value,
            StartAt = competition.StartAt,
            Distance = new DistanceDto
            { 
                Amount = competition.Distance.Amount,
                Unit = competition.Distance.Unit.ToString()
            },
            MaxCompetitors = competition.MaxCompetitors,
            Place = new CompetitionPlaceDto 
            { 
                City = competition.Place.City,
                Latitude = competition.Place.Localization.Latitude,
                Longitute = competition.Place.Localization.Longitude
            },
            Status = competition.Status.ToString(),
            Checkpoints = competition.Checkpoints
                .Select(c => new CheckpointDto { Id = c.Id.Value, TrackPointAmount = c.TrackPoint.Amount, TrackPointUnit = c.TrackPoint.Unit.ToString() })
        };
    }

    public async Task OpenRegistrationAsync(Guid id)
    {
        var competition = await _competitionRepository.GetAsync(CompetitionId.From(id));
        if (competition is null) throw new NotFoundException();

        try
        {
            competition.OpenRegistration();
        }
        catch (CannotOpenRegistrationException)
        {
            throw new ValidationException("Cannot open registration");
        }

        await _competitionRepository.UpdateAsync(competition);
        await _unitOfWork.CommitAsync();
    }

    public async Task CompleteRegistrationAsync(Guid id)
    {
        var competition = await _competitionRepository.GetAsync(CompetitionId.From(id));
        if (competition is null) throw new NotFoundException();

        try
        {
            competition.CompleteRegistration();
        }
        catch (CannotCompleteRegistrationException)
        {
            throw new ValidationException("Cannot complete registration");
        }

        await _competitionRepository.UpdateAsync(competition);
        await _unitOfWork.CommitAsync();
    }

    public async Task ChangeMaxCompetitors(Guid id, int maxCompetitors)
    {
        var competition = await _competitionRepository.GetAsync(CompetitionId.From(id));
        if (competition is null) throw new NotFoundException();

        try
        {
            competition.ChangeMaxCompetitors(maxCompetitors);
        }
        catch (CompetitionMaxCompetitorsChangeNotAllowedException)
        {
            throw new ValidationException("Changing maximum numbers of competitors is not allowed");
        }

        await _competitionRepository.UpdateAsync(competition);
        await _unitOfWork.CommitAsync();
    }

    public async Task AddCheckpoint(Guid competitionId, AddCheckpointRequestDto checkpointDto)
    {
        var competition = await _competitionRepository.GetAsync(CompetitionId.From(competitionId), x => x.Checkpoints);
        if (competition is null) throw new NotFoundException();

        if (!Enum.TryParse<DistanceUnit>(checkpointDto.TrackPointUnit, out var distanceUnit)) throw new ValidationException("Distance unit is incorrect");

        try
        {
            competition.AddCheckpoint(
                new Checkpoint(
                    CheckpointId.From(Guid.NewGuid()),
                    CompetitionId.From(competitionId),
                    new Distance(checkpointDto.TrackPointAmount, distanceUnit)));
        }
        catch (CheckpointAlreadyExistsException)
        {
            throw new ValidationException("Cannot add a checkpoint because checkpoint in this place already exists");
        }

        await _competitionRepository.UpdateAsync(competition);
        await _unitOfWork.CommitAsync();
    }

    public async Task RemoveCheckpoint(Guid competitionId, Guid checkpointId)
    {
        var competition = await _competitionRepository.GetAsync(CompetitionId.From(competitionId), x => x.Checkpoints);
        if (competition is null) throw new NotFoundException();

        try
        {
            competition.RemoveCheckpoint(CheckpointId.From(checkpointId));
        }
        catch (CheckpointNotExistsException)
        {
            throw new ValidationException("Cannot remove a checkpoint because checkpoint does not exist");
        }

        await _competitionRepository.UpdateAsync(competition);
        await _unitOfWork.CommitAsync();
    }
}
