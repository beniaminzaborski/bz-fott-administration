using AutoMapper;
using Bz.Fott.Administration.Application.Common;
using Bz.Fott.Administration.Application.Common.Exceptions;
using Bz.Fott.Administration.Domain.ManagingCompetition;
using Bz.Fott.Administration.Domain.Utils;
using FluentValidation;

namespace Bz.Fott.Administration.Application.Competitions;

internal class CompetitionService : ICompetitionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICompetitionRepository _competitionRepository;
    private readonly IValidator<CreateCompetitionDto> _createCompetitionDtoValidator;
    private readonly IValidator<AddCheckpointRequestDto> _addCheckpointRequestDtoValidator;

    public CompetitionService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICompetitionRepository competitionRepository,
        IValidator<CreateCompetitionDto> createCompetitionDtoValidator,
        IValidator<AddCheckpointRequestDto> addCheckpointRequestDtoValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _competitionRepository = competitionRepository;
        _createCompetitionDtoValidator = createCompetitionDtoValidator;
        _addCheckpointRequestDtoValidator = addCheckpointRequestDtoValidator;
    }

    public async Task<Guid> CreateCompetitionAsync(CreateCompetitionDto dto)
    {
        await _createCompetitionDtoValidator.ValidateAndThrowAsync(dto);

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
        return _mapper.Map<CompetitionDto>(competition);
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
            throw new Common.Exceptions.ValidationException("Cannot open registration");
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
            throw new Common.Exceptions.ValidationException("Cannot complete registration");
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
            throw new Common.Exceptions.ValidationException("Changing maximum numbers of competitors is not allowed");
        }

        await _competitionRepository.UpdateAsync(competition);
        await _unitOfWork.CommitAsync();
    }

    public async Task AddCheckpoint(Guid competitionId, AddCheckpointRequestDto checkpointDto)
    {
        await _addCheckpointRequestDtoValidator.ValidateAndThrowAsync(checkpointDto);

        var competition = await _competitionRepository.GetAsync(CompetitionId.From(competitionId), x => x.Checkpoints);
        if (competition is null) throw new NotFoundException();

        if (!Enum.TryParse<DistanceUnit>(checkpointDto.TrackPointUnit, out var distanceUnit)) throw new Common.Exceptions.ValidationException("Distance unit is incorrect");

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
            throw new Common.Exceptions.ValidationException("Cannot add a checkpoint because checkpoint in this place already exists");
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
            throw new Common.Exceptions.ValidationException("Cannot remove a checkpoint because checkpoint does not exist");
        }

        await _competitionRepository.UpdateAsync(competition);
        await _unitOfWork.CommitAsync();
    }
}
