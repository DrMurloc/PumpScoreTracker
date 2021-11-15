namespace ScoreTracker.Web.Dtos;

using Core.Enums;

public sealed class AttemptDto
{
  public ChartTypeEnum ChartType { get; set; }
  public bool IsPassing { get; set; }
  public string Letter { get; set; } = "";
  public int Level { get; set; }
  public string SongName { get; set; } = "";
}