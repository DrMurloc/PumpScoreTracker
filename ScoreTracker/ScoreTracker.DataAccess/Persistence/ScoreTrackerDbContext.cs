namespace ScoreTracker.DataAccess.Persistence;

using Entities;
using Microsoft.EntityFrameworkCore;

public sealed class ScoreTrackerDbContext : DbContext
{
  #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

  public ScoreTrackerDbContext(DbContextOptions<ScoreTrackerDbContext> options) : base(options) { }

  public DbSet<ChartEntity> Chart { get; set; }
  public DbSet<SongEntity> Song { get; set; }
  public DbSet<AttemptEntity> Attempt { get; set; }
  public DbSet<UserEntity> User { get; set; }
  #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}