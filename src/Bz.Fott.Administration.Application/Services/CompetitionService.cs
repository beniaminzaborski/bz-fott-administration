using Bz.Fott.Administration.Application.Common;
using Bz.Fott.Administration.Domain.ManagingCompetition;
using Bz.Fott.Administration.Domain.Utils;

namespace Bz.Fott.Administration.Application.Services;

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

    public async Task<CompetitionId> CreateCompetitionAsync()
    {
        // TODO: Validate DTO

        // TODO: Map DTO to Entity
        var competition =new Competition(
            CompetitionId.From(new Guid("0c33c4ad-bbd3-4c94-acac-ab1907146834")),
            DistanceHelper.Marathon(),
            new DateTime(2032, 02, 08, 10, 00, 00),
            8000,
            new CompetitionPlace("Kielce", 123, 321));

        competition.Approve();

        await _competitionRepository.CreateAsync(competition);
        await _unitOfWork.CommitAsync();

        return competition.Id;
    }
}
