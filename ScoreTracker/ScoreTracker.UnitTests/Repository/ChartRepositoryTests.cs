namespace ScoreTracker.UnitTests.Repository;

using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using DataAccess;
using DataAccess.Entities;
using Helpers;
using Xunit;

public sealed class ChartRepositoryTests
{
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
    Assert.Equal(song1.Name, result[0].SongName);
    Assert.Equal(chart1.Level, result[0].Level);
    Assert.Equal(chart1.Type, result[0].Type.ToString());
    Assert.Equal(song2.Name, result[1].SongName);
    Assert.Equal(chart2.Level, result[1].Level);
    Assert.Equal(chart2.Type, result[1].Type.ToString());
  }
}