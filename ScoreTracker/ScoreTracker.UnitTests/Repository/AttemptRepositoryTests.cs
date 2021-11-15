namespace ScoreTracker.UnitTests.Repository;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Core.Models;
using DataAccess;
using DataAccess.Entities;
using Domain.Models;
using Domain.ValueTypes;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

public sealed class AttemptRepositoryTests : IDisposable
{
  public void Dispose()
  {
    var context = DbContextBuilder.BuildContext();
    context.User.RemoveRange(context.User.ToArray());
    context.Chart.RemoveRange(context.Chart.ToArray());
    context.Song.RemoveRange(context.Song.ToArray());
    context.Attempt.RemoveRange(context.Attempt.ToArray());
    context.SaveChanges();
  }

  private readonly Fixture _fixture = FixtureBuilder.BuildFixture();

  [Fact]
  public async Task RegisterAttemptCreatesAttemptEntry()
  {
    var user = _fixture.Create<User>();
    var userId = Guid.NewGuid();
    var chart = _fixture.Create<Chart>();
    var chartId = Guid.NewGuid();
    var songId = Guid.NewGuid();
    var grade = _fixture.Create<GradeValueType>();

    var dbContext = DbContextBuilder.BuildContext();
    dbContext.User.Add(new UserEntity { Email = user.Email, Id = userId });
    dbContext.Song.Add(new SongEntity { Id = songId, Name = chart.SongName });
    dbContext.Chart.Add(new ChartEntity { Id = chartId, Level = chart.Level, SongId = songId, Type = chart.Type.ToString() });
    await dbContext.SaveChangesAsync();

    var repository = new AttemptRepository(dbContext);

    //Test
    await repository.CreateAttempt(user, chart, grade, CancellationToken.None);

    //Assert
    var attempt = await dbContext.Attempt.SingleAsync();
    Assert.Equal(chartId, attempt.ChartId);
    Assert.NotEqual(default, attempt.Id);
    Assert.Equal(userId, attempt.UserId);
    Assert.Equal(grade.Letter, attempt.Letter);
    Assert.Equal(grade.IsPassing, attempt.IsPassing);
  }
}