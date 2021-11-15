namespace ScoreTracker.UnitTests.Handlers;

using System.Threading;
using System.Threading.Tasks;
using Application;
using Application.Contracts;
using AutoFixture;
using Core.Models;
using Domain.Models;
using Domain.ValueTypes;
using FakeItEasy;
using Helpers;
using Mediation.Commands;
using Xunit;

public sealed class CreateChartAttemptTests
{
  private readonly Fixture _fixture = FixtureBuilder.BuildFixture();

  [Fact]
  public async Task RegistersAttempt()
  {
    //Setup
    var chart = _fixture.Create<Chart>();
    var user = _fixture.Create<User>();
    var grade = _fixture.Create<GradeValueType>();

    var userAccessor = A.Fake<ICurrentUserAccessor>();
    var chartRepository = A.Fake<IChartRepository>();
    var attemptRepository = A.Fake<IAttemptRepository>();

    var handler = new CreateChartAttemptHandler(chartRepository, userAccessor, attemptRepository);

    A.CallTo(() => userAccessor.CurrentUser).Returns(user);
    A.CallTo(() => chartRepository.GetChart(chart.SongName, chart.Type, chart.Level, A<CancellationToken>.Ignored)).Returns(chart);

    //Test
    await handler.Handle(new CreateChartAttemptCommand(chart.SongName, chart.Type, chart.Level, grade), CancellationToken.None);

    //Assert
    A.CallTo(() => attemptRepository.CreateAttempt(user, chart, grade, A<CancellationToken>.Ignored)).MustHaveHappenedOnceExactly();
  }
}