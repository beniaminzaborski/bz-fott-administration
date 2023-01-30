using Bz.Fott.Administration.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bz.Fott.Administration.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompetitionController : ControllerBase
{
    private readonly ICompetitionService _competitionService;

    public CompetitionController(ICompetitionService competitionService)
    {
        _competitionService = competitionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        await _competitionService.CreateCompetitionAsync();

        return Ok();
    }
}
