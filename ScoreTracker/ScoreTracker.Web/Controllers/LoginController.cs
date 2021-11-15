namespace ScoreTracker.Web.Controllers;

using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

[ExcludeFromCodeCoverage]
[Route("login")]
public sealed class LoginController : Controller
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public LoginController(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  [HttpGet("callback")]
  public async Task<IActionResult> Callback([FromQuery] string scheme)
  {
    try
    {
      var authenticateResult = await HttpContext.AuthenticateAsync(scheme);
      var providerScheme = authenticateResult.Ticket?.Properties.Items["scheme"] ?? "";
      var returnUrl = "";
      authenticateResult.Ticket?.Properties.Items.TryGetValue("returnUrl", out returnUrl);
      returnUrl = Url.Action("Index", "Home") + "#" + returnUrl;
      var externalClaims = authenticateResult.Principal?.Claims ?? Array.Empty<Claim>();
      var email = getEmail(externalClaims, providerScheme);

      var principal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }));
      var httpContext = _httpContextAccessor.HttpContext;
      if (httpContext == null)
      {
        return Redirect(returnUrl);
      }

      await httpContext.SignOutAsync();
      await httpContext.SignInAsync(principal);
      return Redirect(returnUrl);
    }
    catch (Exception)
    {
      return RedirectToAction("Index", "Home");
    }
  }

  [HttpGet("{providerScheme}")]
  public IActionResult ExternalLogin([FromRoute] string providerScheme, [FromQuery] string returnUrl)
  {
    if (string.IsNullOrWhiteSpace(returnUrl))
    {
      returnUrl = "/";
    }

    var redirectUri = Url.Action("Callback", new { scheme = providerScheme });
    var props = new AuthenticationProperties { RedirectUri = redirectUri, Items = { { "scheme", providerScheme }, { "returnUrl", returnUrl } } };
    return new ChallengeResult(providerScheme, props);
  }

  #region Support Methods

  private static string getEmail(IEnumerable<Claim> claims, string providerName)
  {
    var claimName = providerName == "Google" ? ClaimTypes.Email : "";
    return claims.FirstOrDefault(c => c.Type == claimName)?.Value ?? "";
  }

  #endregion
}