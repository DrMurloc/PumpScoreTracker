namespace ScoreTracker.DataAccess;

using Application.Contracts;
using Core.Models;
using Domain.Models;
using Domain.ValueTypes;
using Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;

public sealed class AttemptRepository : IAttemptRepository
{
  private readonly ScoreTrackerDbContext _dbContext;

  public AttemptRepository(ScoreTrackerDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task CreateAttempt(User user, Chart chart, GradeValueType grade, CancellationToken cancellationToken = default)
  {
    var userId = (await _dbContext.User.SingleAsync(u => u.Email == user.Email, cancellationToken)).Id;
    var chartId = await (from s in _dbContext.Song
      join c in _dbContext.Chart on s.Id equals c.SongId
      where s.Name == chart.SongName && c.Level == chart.Level && c.Type == chart.Type.ToString()
      select c.Id).SingleAsync(cancellationToken);

    var newAttempt = new AttemptEntity
    {
      Id = Guid.NewGuid(),
      ChartId = chartId,
      IsPassing = grade.IsPassing,
      Letter = grade.Letter,
      UserId = userId
    };
    await _dbContext.Attempt.AddAsync(newAttempt, cancellationToken);
    await _dbContext.SaveChangesAsync(cancellationToken);
  }
}