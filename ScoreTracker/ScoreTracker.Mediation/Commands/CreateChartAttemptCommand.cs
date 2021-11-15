namespace ScoreTracker.Mediation.Commands;

using Core.Enums;
using Domain.ValueTypes;
using MediatR;

public sealed class CreateChartAttemptCommand : IRequest
{
  public CreateChartAttemptCommand(string songName, ChartTypeEnum chartType, int level, GradeValueType grade)
  {
    SongName = songName;
    ChartType = chartType;
    Level = level;
    Grade = grade;
  }

  public ChartTypeEnum ChartType { get; }
  public GradeValueType Grade { get; }
  public int Level { get; }
  public string SongName { get; }
}