namespace ScoreTracker.CompositionRoot;

using System.Diagnostics.CodeAnalysis;
using Application;
using Application.Contracts;
using DataAccess;
using DataAccess.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class RegistrationExtensions
{
  public static IServiceCollection AddScoreTrackerCore(this IServiceCollection builder)
  {
    return builder.AddMediatR(typeof(GetChartsHandler));
  }

  public static IServiceCollection AddScoreTrackerInfrastructure(this IServiceCollection builder)
  {
    builder.AddDbContext<ScoreTrackerDbContext>(o => o.UseInMemoryDatabase("ScoreTracker"));
    foreach (var repository in typeof(ChartRepository).Assembly.GetTypes())
    {
      foreach (var coreInterface in repository.GetInterfaces().Where(i => i.Assembly == typeof(IChartRepository).Assembly).ToArray())
      {
        builder.AddTransient(coreInterface, repository);
      }
    }

    return builder;
  }
}