namespace ScoreTracker.Web.Dtos;

public sealed class ChartDto
{
  public int Level { get; set; }
  public string SongName { get; set; } = "";
  public string Type { get; set; } = "";
}