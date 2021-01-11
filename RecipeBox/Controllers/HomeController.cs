using Microsoft.AspNetCore.Mvc;

namespace RecipeBox.Controllers
{
  public class HomeController : Controllers
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}