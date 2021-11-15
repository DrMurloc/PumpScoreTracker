namespace ScoreTracker.DataAccess.Entities;

public sealed class SongEntity
{
  public Guid Id { get; set; }
  public string Name { get; set; } = "";
}