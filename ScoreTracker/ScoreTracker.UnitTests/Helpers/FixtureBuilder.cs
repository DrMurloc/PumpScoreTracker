namespace ScoreTracker.UnitTests.Helpers;

using System;
using AutoFixture;
using Core.Enums;
using DataAccess.Entities;
using Domain.ValueTypes;

public static class FixtureBuilder
{
  private static readonly string[] _letters = { "A", "B", "C", "D", "F", "S", "SS", "SSS" };
  private static readonly Random _random = new();

  public static Fixture BuildFixture()
  {
    var fixture = new Fixture();
    fixture.Customize<ChartEntity>(c => c.With(m => m.Type, getRandomEnumValue<ChartTypeEnum>));
    fixture.Customize<GradeValueType>(c => c.FromFactory(getRandomLetterGrade));
    return fixture;
  }

  #region Support Methods

  private static string getRandomEnumValue<T>()
    where T : struct, Enum
  {
    var values = Enum.GetValues<T>();
    var index = _random.Next(0, values.Length - 1);
    return values.GetValue(index)?.ToString() ?? "";
  }

  private static GradeValueType getRandomLetterGrade()
  {
    var letterIndex = _random.Next(0, _letters.Length - 1);
    var isPassing = _random.Next(0, 1) == 0;
    GradeValueType.TryParse(_letters[letterIndex], isPassing, out var result);
    return result;
  }

  #endregion
}