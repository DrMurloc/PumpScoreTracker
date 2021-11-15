namespace ScoreTracker.UnitTests.Repository;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Core.Enums;
using DataAccess;
using DataAccess.Entities;
using Domain.Exceptions;
using Helpers;
using Xunit;

public sealed class ChartRepositoryTests : IDisposable
{
  public void Dispose()
  {
    var dbContext = DbContextBuilder.BuildContext();
    dbContext.Chart.RemoveRange(dbContext.Chart.ToArray());
    dbContext.Song.RemoveRange(dbContext.Song.ToArray());
    dbContext.SaveChanges();
  }

  private readonly Fixture _fixture = FixtureBuilder.BuildFixture();

  [Fact]
  public async Task EntitiesMap()
  {
    //Setup
    var chart1 = _fixture.Create<ChartEntity>();
    var song1 = _fixture.Create<SongEntity>();

    chart1.SongId = song1.Id;

    var chart2 = _fixture.Create<ChartEntity>();
    var song2 = _fixture.Create<SongEntity>();

    chart2.SongId = song2.Id;

    var dbContext = DbContextBuilder.BuildContext();
    var repository = new ChartRepository(dbContext);

    await dbContext.Song.AddRangeAsync(song1, song2);
    await dbContext.Chart.AddRangeAsync(chart1, chart2);
    await dbContext.SaveChangesAsync();

    //Test
    var result = (await repository.GetAllCharts()).ToArray();

    //Assert
    Assert.Equal(2, result.Length);
    var result1 = result.Single(r => r.SongName == song1.Name);
    var result2 = result.Single(r => r.SongName == song2.Name);
    Assert.Equal(song1.Name, result1.SongName);
    Assert.Equal(chart1.Level, result1.Level);
    Assert.Equal(chart1.Type, result1.Type.ToString());
    Assert.Equal(song2.Name, result2.SongName);
    Assert.Equal(chart2.Level, result2.Level);
    Assert.Equal(chart2.Type, result2.Type.ToString());
  }

  [Fact]
  public async Task GetChartRetrievesSpecifiedChart()
  {
    //Setup
    var chart1 = _fixture.Create<ChartEntity>();
    var song1 = _fixture.Create<SongEntity>();

    chart1.SongId = song1.Id;

    var chart2 = _fixture.Create<ChartEntity>();
    var song2 = _fixture.Create<SongEntity>();

    chart2.SongId = song2.Id;

    var dbContext = DbContextBuilder.BuildContext();
    var repository = new ChartRepository(dbContext);

    await dbContext.Song.AddRangeAsync(song1, song2);
    await dbContext.Chart.AddRangeAsync(chart1, chart2);
    await dbContext.SaveChangesAsync();

    //Test
    Enum.TryParse<ChartTypeEnum>(chart2.Type, out var type);
    var result = await repository.GetChart(song2.Name, type, chart2.Level, CancellationToken.None);

    //Assert
    Assert.NotNull(result);
    Assert.Equal(song2.Name, result.SongName);
    Assert.Equal(chart2.Level, result.Level);
    Assert.Equal(chart2.Type, result.Type.ToString());
  }

  [Fact]
  public async Task NotFoundChartThrowsException()
  {
    var dbContext = DbContextBuilder.BuildContext();
    var repository = new ChartRepository(dbContext);

    //Test
    await Assert.ThrowsAsync<ChartNotFoundException>(() => repository.GetChart("Some Song", ChartTypeEnum.Single, 11, CancellationToken.None));
  }
}