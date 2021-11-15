namespace ScoreTracker.Domain.Exceptions;

using Core.Enums;

public sealed class ChartNotFoundException : Exception
{
  public ChartNotFoundException(string songName, ChartTypeEnum chartType, int level) : base($"Chart not found: {songName}, {chartType}, {level}") { }
}