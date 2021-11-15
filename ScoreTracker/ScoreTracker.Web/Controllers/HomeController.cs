namespace ScoreTracker.Web.Controllers;

using Microsoft.AspNetCore.Mvc;

[Route("")]
public sealed class HomeController : Controller
{
  [HttpGet("")]
  public IActionResult Index()
  {
    return Json(new { Result = "Ok!" });
  }
}