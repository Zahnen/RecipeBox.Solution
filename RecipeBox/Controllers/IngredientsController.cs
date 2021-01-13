using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace RecipeBox.Controllers
{
  [Authorize]
  public class IngredientsController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public IngredientsController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }


    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]  
    public ActionResult Create (Ingredient ingredient)
    {
      var check = _db.Ingredients
        .Any(record=>record.IngredientName == ingredient.IngredientName);
      if(!check)
      {
        _db.Ingredients.Add(ingredient);
        _db.SaveChanges();
      }
      return RedirectToAction("Index", "Recipes");
    }
  }
}