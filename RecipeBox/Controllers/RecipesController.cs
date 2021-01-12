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
  [Authorize] // this makes all routes only viewable when logged in
  public class RecipesController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public RecipesController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userRecipes = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id);
      return View(userRecipes);
    }


    public ActionResult Create()
    {
      ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Name");
      ViewBag.Tags = _db.Tags.ToList();
      return View();
    }

    [HttpPost]  
    public async Task<ActionResult> Create (Recipe recipe, int TagId, int IngredientId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      recipe.User = currentUser;
      _db.Recipes.Add(recipe);
      if(TagId != 0) //if there's a Tag, add it to Recipe
      {
        _db.RecipeTag.Add(new RecipeTag() {TagId = TagId, RecipeId = recipe.RecipeId});
      }
      if(IngredientId != 0) //if there's Ingredient, add it to Recipe
      {
        _db.RecipeIngredient.Add(new RecipeIngredient() {IngredientId = IngredientId, RecipeId = recipe.RecipeId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}