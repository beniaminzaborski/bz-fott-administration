using Bz.Fott.Administration.Application.Competitions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateAsync()
    {
        var id = await _competitionService.CreateCompetitionAsync();
        return CreatedAtAction(nameof(GetAsync), new { id }, null);
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(CompetitionDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAsync(Guid id)
    { 
        var dto = await _competitionService.GetCompetitionAsync(id);
        return Ok(dto);
    }

    [HttpPost("{id:Guid}/Registration/Open")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> OpenRegistrationAsync(Guid id)
    {
        await _competitionService.OpenRegistrationAsync(id);
        return NoContent();
    }

    [HttpPost("{id:Guid}/Registration/Complete")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> CompleteRegistrationAsync(Guid id)
    {
        await _competitionService.CompleteRegistrationAsync(id);
        return NoContent();
    }
}
