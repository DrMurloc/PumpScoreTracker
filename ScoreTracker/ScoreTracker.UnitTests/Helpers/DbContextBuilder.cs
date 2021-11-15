namespace ScoreTracker.UnitTests.Helpers;

using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

public static class DbContextBuilder
{
  public static ScoreTrackerDbContext BuildContext()
  {
    return new ScoreTrackerDbContext(new DbContextOptionsBuilder<ScoreTrackerDbContext>().UseInMemoryDatabase("ScoreTracker").Options);
  }
}