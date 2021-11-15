namespace ScoreTracker.UnitTests.Controllers;

using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Core.Models;
using FakeItEasy;
using Helpers;
using Mediation.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Controllers.Api;
using Web.Dtos;
using Xunit;

public sealed class ChartControllerTests
{
  private readonly Fixture _fixture = FixtureBuilder.BuildFixture();

  [Fact]
  public async Task GetAllReturnsCharts()
  {
    //Setup
    var chart1 = _fixture.Create<Chart>();
    var chart2 = _fixture.Create<Chart>();

    var mediator = A.Fake<IMediator>();

    A.CallTo(() => mediator.Send(A<GetChartsQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(new[] { chart1, chart2 });

    var controller = new ChartController(mediator, A.Fake<IHttpContextAccessor>(), MapperBuilder.BuildMapper());

    //Test
    var result = await controller.GetAllCharts();

    //Assert
    Assert.IsAssignableFrom<JsonResult>(result);
    var jsonResult = (JsonResult)result;
    var expected = JsonConvert.SerializeObject
    (new[]
    {
      new ChartDto { Level = chart1.Level, SongName = chart1.SongName, Type = chart1.Type.ToString() },
      new ChartDto { Level = chart2.Level, SongName = chart2.SongName, Type = chart2.Type.ToString() }
    });
    var actual = JsonConvert.SerializeObject(jsonResult.Value);

    Assert.Equal(expected, actual);
  }
}