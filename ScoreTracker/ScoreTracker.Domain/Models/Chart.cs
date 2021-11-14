namespace ScoreTracker.Core.Models;

using Enums;

public sealed class Chart
{
  public Chart(string songName, ChartTypeEnum type, int level)
  {
    SongName = songName;
    Level = level;
    Type = type;
  }

  public int Level { get; }
  public string SongName { get; }
  public ChartTypeEnum Type { get; }
}