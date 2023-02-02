using Bz.Fott.Administration.Application.Common;
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

    public async Task<Guid> CreateCompetitionAsync()
    {
        // TODO: Validate DTO

        // TODO: Map DTO to Entity
        var competition = new Competition(
            CompetitionId.From(Guid.NewGuid()),
            DistanceHelper.Marathon(),
            new DateTime(2032, 02, 08, 10, 00, 00),
            8000,
            new CompetitionPlace("Kielce", new Geolocalization(50.86022655378784m, 20.623838070358033m)));

        competition.OpenRegistration();

        await _competitionRepository.CreateAsync(competition);
        await _unitOfWork.CommitAsync();

        return competition.Id.Value;
    }

    public async Task<CompetitionDto> GetCompetitionAsync(Guid id)
    {
        var competition = await _competitionRepository.GetAsync(CompetitionId.From(id), i => i.Checkpoints);
        if (competition is null) throw new Exception(); //NotFoundException("id");
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
            Checkpoints = competition.Checkpoints.Select(c => new CheckpointDto { Id = c.Id.Value, TrackPointAmount = c.TrackPoint.Amount, TrackPointUnit = c.TrackPoint.Unit.ToString() })
        };
    }

    public async Task OpenRegistrationAsync(Guid id)
    {
        var competition = await _competitionRepository.GetAsync(CompetitionId.From(id));
        if (competition is null) throw new Exception(); //NotFoundException("id");

        competition.OpenRegistration();

        await _competitionRepository.UpdateAsync(competition);
        await _unitOfWork.CommitAsync();
    }

    public async Task CompleteRegistrationAsync(Guid id)
    {
        var competition = await _competitionRepository.GetAsync(CompetitionId.From(id));
        if (competition is null) throw new Exception(); //NotFoundException("id");

        competition.CompleteRegistration();

        await _competitionRepository.UpdateAsync(competition);
        await _unitOfWork.CommitAsync();
    }
}
