namespace ScoreTracker.Application;

using Contracts;
using Core.Models;
using Mediation.Queries;
using MediatR;

public sealed class GetChartsHandler : IRequestHandler<GetChartsQuery, IEnumerable<Chart>>
{
  private readonly IChartRepository _chartRepository;

  public GetChartsHandler(IChartRepository chartRepository)
  {
    _chartRepository = chartRepository;
  }

  public async Task<IEnumerable<Chart>> Handle(GetChartsQuery request, CancellationToken cancellationToken)
  {
    return await _chartRepository.GetAllCharts(cancellationToken);
  }
}