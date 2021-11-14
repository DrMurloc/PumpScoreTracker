namespace ScoreTracker.Application.Contracts;

using Core.Models;

public interface IChartRepository
{
  Task<IEnumerable<Chart>> GetAllCharts(CancellationToken cancellationToken = default);
}