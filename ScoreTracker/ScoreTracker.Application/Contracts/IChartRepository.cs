namespace ScoreTracker.Application.Contracts;

using Core.Enums;
using Core.Models;

public interface IChartRepository
{
  Task<IEnumerable<Chart>> GetAllCharts(CancellationToken cancellationToken = default);

  Task<Chart> GetChart(string songName, ChartTypeEnum chartType, int level, CancellationToken cancellationToken = default);
}