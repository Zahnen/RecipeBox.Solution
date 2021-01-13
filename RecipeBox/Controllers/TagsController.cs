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
  public class TagsController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TagsController(RecipeBoxContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }
    public ActionResult Index()
    {
      List<Tag> model = _db.Tags.ToList();
      return View(model);
    }
    [Authorize]
    public ActionResult Create()
    {
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Tag tag)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      tag.User = currentUser;
      _db.Tags.Add(tag);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTag = _db.Tags
        .Include(tag => tag.Recipes)
        .ThenInclude(join => join.Recipe)
        .FirstOrDefault(tag => tag.TagId == id);
      return View(thisTag);
    }

    [Authorize]
    public async Task<ActionResult> Edit(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisTag = _db.Tags.Where(entry => entry.User.Id == currentUser.Id).FirstOrDefault(tag => tag.TagId == id);
      if(thisTag == null)
      {
        return RedirectToAction("Details", new {id = id});
      }
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name");
      return View(thisTag);
    }

    [HttpPost]
    public ActionResult Edit(Tag tag)
    {
      _db.Entry(tag).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index"); //potentially route back to edited Tag details
    }

    [Authorize]
    public async Task<ActionResult> Delete(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisTag = _db.Tags.Where(entry => entry.User.Id == currentUser.Id).FirstOrDefault(tag => tag.TagId == id);
      if(thisTag == null)
      {
        return RedirectToAction("Details", new {id = id});
      }
      return View(thisTag);
    }


    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
      _db.Tags.Remove(thisTag);
      _db.SaveChanges();
      return RedirectToAction("Index", "Recipes");
    }

    [HttpPost]
    public ActionResult DeleteRecipe(int joinId, int TagId)
    {
      var thisEntry = _db.RecipeTag.FirstOrDefault(record => record.RecipeTagId == joinId);
      _db.RecipeTag.Remove(thisEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", "Tags", new{id=TagId});
    }

    [Authorize]
    public ActionResult AddRecipe(int id)
    {
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name");
      return View();
    }

    [HttpPost] 
    public ActionResult AddRecipe(Tag tag, int RecipeId)
    {
      if(RecipeId !=0)
      {
        var thisrecord = _db.RecipeTag  
          .Any(record=>record.RecipeId == RecipeId && record.TagId == tag.TagId);
        if(!thisrecord)
        {
          _db.RecipeTag.Add(new RecipeTag(){RecipeId=RecipeId, TagId=tag.TagId});
        }
      }
      _db.SaveChanges();
      return RedirectToAction("Details", "Recipes", new{id=RecipeId});
    }
  }
}