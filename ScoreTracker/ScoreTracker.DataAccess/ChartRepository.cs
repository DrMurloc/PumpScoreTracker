namespace ScoreTracker.DataAccess;

using Application.Contracts;
using Core.Enums;
using Core.Models;
using Domain.Exceptions;
using Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class ChartRepository : IChartRepository
{
  private readonly ScoreTrackerDbContext _dbContext;

  public ChartRepository(ScoreTrackerDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IEnumerable<Chart>> GetAllCharts(CancellationToken cancellationToken = default)
  {
    return await (from chart in _dbContext.Chart join song in _dbContext.Song on chart.SongId equals song.Id select entitiesToChart(song, chart))
      .ToArrayAsync(cancellationToken);
  }

  public async Task<Chart> GetChart(string songName, ChartTypeEnum chartType, int level, CancellationToken cancellationToken = default)
  {
    var result = await (from song in _dbContext.Song
      join chart in _dbContext.Chart on song.Id equals chart.SongId
      where song.Name == songName && chart.Type == chartType.ToString() && chart.Level == level
      select entitiesToChart(song, chart)).FirstOrDefaultAsync(cancellationToken);
    if (result == null)
    {
      throw new ChartNotFoundException(songName, chartType, level);
    }

    return result;
  }

  #region Support Methods

  private static Chart entitiesToChart(SongEntity song, ChartEntity chart)
  {
    return new Chart(song.Name, Enum.TryParse<ChartTypeEnum>(chart.Type, out var value) ? value : ChartTypeEnum.Single, chart.Level);
  }

  #endregion
}