namespace ScoreTracker.UnitTests.Controllers;

using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Domain.ValueTypes;
using FakeItEasy;
using Helpers;
using Mediation.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Api;
using Web.Dtos;
using Xunit;

public sealed class AttemptControllerTests
{
  private readonly Fixture _fixture = FixtureBuilder.BuildFixture();

  [InlineData("z")]
  [InlineData("AA")]
  [InlineData("SSSS")]
  [Theory]
  public async Task InvalidGradeLetterReturnsBadRequest(string letter)
  {
    //Setup
    var controller = new AttemptController(A.Fake<IMediator>(), A.Fake<IHttpContextAccessor>());

    var attempt = _fixture.Create<AttemptDto>();
    attempt.Letter = letter;

    //Test
    var result = await controller.CreateAttempt(attempt);

    //Assert
    Assert.IsAssignableFrom<BadRequestObjectResult>(result);
  }

  [Fact]
  public async Task CreateAttemptFiresCommand()
  {
    //Setup
    var grade = _fixture.Create<GradeValueType>();
    var attempt = _fixture.Create<AttemptDto>();
    attempt.Letter = grade.Letter;
    var mediator = A.Fake<IMediator>();

    var controller = new AttemptController(mediator, A.Fake<IHttpContextAccessor>());

    //Test
    var result = await controller.CreateAttempt(attempt);

    //Assert
    A.CallTo
    (() => mediator.Send
    (A<CreateChartAttemptCommand>.That.Matches
    (c => c.SongName == attempt.SongName && c.ChartType == attempt.ChartType && c.Grade.Letter == attempt.Letter
          && c.Grade.IsPassing == attempt.IsPassing), A<CancellationToken>.Ignored)).MustHaveHappenedOnceExactly();

    Assert.IsAssignableFrom<OkResult>(result);
  }
}