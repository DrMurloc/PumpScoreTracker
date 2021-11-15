namespace ScoreTracker.DataAccess.Entities;

public class AttemptEntity
{
  public Guid ChartId { get; set; }
  public Guid Id { get; set; }
  public bool IsPassing { get; set; }
  public string Letter { get; set; } = "";
  public Guid UserId { get; set; }
}