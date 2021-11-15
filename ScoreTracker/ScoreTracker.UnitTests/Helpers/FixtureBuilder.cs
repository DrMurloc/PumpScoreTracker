namespace ScoreTracker.UnitTests.Helpers;

using System;
using AutoFixture;
using Core.Enums;
using DataAccess.Entities;

public static class FixtureBuilder
{
  private static readonly Random _random = new();

  public static Fixture BuildFixture()
  {
    var fixture = new Fixture();
    fixture.Customize<ChartEntity>(c => c.With(m => m.Type, getRandomEnumValue<ChartTypeEnum>));
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

  #endregion
}