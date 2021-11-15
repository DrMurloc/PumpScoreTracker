namespace ScoreTracker.Web.Controllers;

using AutoMapper;
using Dtos;
using Mediation.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public sealed class ChartController : Controller
{
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IMapper _mapper;
  private readonly IMediator _mediator;

  public ChartController(IMediator mediator, IHttpContextAccessor httpContextAccessor, IMapper mapper)
  {
    _mediator = mediator;
    _httpContextAccessor = httpContextAccessor;
    _mapper = mapper;
  }

  private CancellationToken RequestCancelled => _httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;

  [HttpGet]
  public async Task<IActionResult> GetAllCharts()
  {
    var charts = await _mediator.Send(new GetChartsQuery(), RequestCancelled);
    return Json(charts.Select(_mapper.Map<ChartDto>));
  }
}