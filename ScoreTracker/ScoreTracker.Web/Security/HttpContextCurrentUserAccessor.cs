namespace ScoreTracker.Web.Security;

using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Application.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;

[ExcludeFromCodeCoverage]
public sealed class HttpContextCurrentUserAccessor : ICurrentUserAccessor
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public HttpContextCurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public User CurrentUser
  {
    get
    {
      var user = _httpContextAccessor.HttpContext?.User;
      if (user == null)
      {
        throw new UserNotLoggedInException();
      }

      return user.Claims.Any() ? new User(user.FindFirstValue(ClaimTypes.Email)) : new User("Anonymous");
    }
  }

  public async Task SetCurrentUser(User user)
  {
    var principal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, user.Email) }));
    var httpContext = _httpContextAccessor.HttpContext;
    if (httpContext == null)
    {
      return;
    }

    await httpContext.SignOutAsync();
    await httpContext.SignInAsync(principal);
  }
}