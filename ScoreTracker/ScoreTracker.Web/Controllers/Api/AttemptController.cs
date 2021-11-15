namespace ScoreTracker.Web.Controllers.Api;

using Domain.ValueTypes;
using Dtos;
using Mediation.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public sealed class AttemptController : Controller
{
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IMediator _mediator;

  public AttemptController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
  {
    _mediator = mediator;
    _httpContextAccessor = httpContextAccessor;
  }

  private CancellationToken RequestAborted => _httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;

  [HttpPost]
  public async Task<IActionResult> CreateAttempt([FromBody] AttemptDto attempt)
  {
    if (!GradeValueType.TryParse(attempt.Letter, attempt.IsPassing, out var grade))
    {
      return BadRequest("Grade is invalid");
    }

    await _mediator.Send(new CreateChartAttemptCommand(attempt.SongName, attempt.ChartType, attempt.Level, grade), RequestAborted);
    return Ok();
  }
}