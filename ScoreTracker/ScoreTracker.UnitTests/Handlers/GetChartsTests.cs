namespace ScoreTracker.UnitTests.Handlers;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application;
using Application.Contracts;
using AutoFixture;
using Core.Models;
using FakeItEasy;
using Mediation.Queries;
using Xunit;

public sealed class GetChartsTests
{
  private readonly Fixture _fixture = new();

  [Fact]
  public async Task GetChartsReturnsAllCharts()
  {
    //Setup
    var chart1 = _fixture.Create<Chart>();
    var chart2 = _fixture.Create<Chart>();

    var repository = A.Fake<IChartRepository>();

    A.CallTo(() => repository.GetAllCharts(A<CancellationToken>.Ignored)).Returns(new[] { chart1, chart2 });

    var handler = new GetChartsHandler(repository);
    //Test
    var result = (await handler.Handle(new GetChartsQuery(), CancellationToken.None)).ToArray();

    //Assert
    Assert.Equal(2, result.Length);
    Assert.Equal(chart1, result[0]);
    Assert.Equal(chart2, result[1]);
  }
}