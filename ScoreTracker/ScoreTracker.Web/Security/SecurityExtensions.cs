namespace ScoreTracker.Web.Security;

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication;

[ExcludeFromCodeCoverage]
public static class SecurityExtensions
{
  public static IServiceCollection AddScoreTrackerSecurity(this IServiceCollection builder, IConfiguration configuration)
  {
    return builder.AddAuthorization().AddAuthentication(ScoreTrackerSecurity.AuthenticationScheme).AddCookie
      (ScoreTrackerSecurity.AuthenticationScheme, options => { options.ExpireTimeSpan = TimeSpan.FromDays(30); }).addProviders
      (configuration).Services;
  }

  #region Support Methods

  private static AuthenticationBuilder addProviders(this AuthenticationBuilder builder, IConfiguration configuration)
  {
    var google = configuration.GetSection("Authentication")?.GetSection("Google");
    if (google?["ClientId"] != null && google["ClientSecret"] != null)
    {
      builder.AddGoogle
      (o =>
      {
        o.ClientId = google["ClientId"];
        o.ClientSecret = google["ClientSecret"];
      });
    }

    return builder;
  }

  #endregion
}