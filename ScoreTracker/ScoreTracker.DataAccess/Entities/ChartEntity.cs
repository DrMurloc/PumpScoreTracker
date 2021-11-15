namespace ScoreTracker.DataAccess.Entities;

public sealed class ChartEntity
{
  public Guid Id { get; set; }
  public int Level { get; set; }
  public Guid SongId { get; set; }
  public string Type { get; set; } = "";
}