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
using System;

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
      ViewBag.Tags =_db.Tags.ToList();
      // ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "IngredientName");
      return View();
    }

    [HttpPost]  
    public async Task<ActionResult> Create (Recipe recipe, int TagId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      recipe.User = currentUser;
      _db.Recipes.Add(recipe);
      if(TagId != 0) //if there's a Tag, add it to Recipe
      {
        _db.RecipeTag.Add(new RecipeTag() {TagId = TagId, RecipeId = recipe.RecipeId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisRecipe = _db.Recipes
        .Include(recipe => recipe.Tags)
        .ThenInclude(join => join.Tag)
        .FirstOrDefault(recipe => recipe.RecipeId == id);
      char[] separators = new char[] { '-', ','};
      ViewBag.SplitIngredients = thisRecipe.Ingredients.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
      ViewBag.Instructions = thisRecipe.Instructions.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
      return View(thisRecipe);
    }

    public async Task<ActionResult> Edit(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisRecipe = _db.Recipes
        .Where(entry => entry.User.Id == currentUser.Id)
        .FirstOrDefault(recipe=>recipe.RecipeId == id);
      if(thisRecipe == null)
      {
        return RedirectToAction ("Details", new{id=id});
      }
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult Edit(Recipe recipe)
    {
      _db.Entry(recipe).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new{id=recipe.RecipeId});
    }

    public async Task<ActionResult> Delete(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisRecipe = _db.Recipes
        .Where(entry=>entry.User.Id == currentUser.Id)
        .FirstOrDefault(recipe=>recipe.RecipeId == id);
      if(thisRecipe == null)
      {
        return RedirectToAction("Details", new {id=id});
      }
      return View(thisRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe=>recipe.RecipeId == id);
      _db.Recipes.Remove(thisRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}