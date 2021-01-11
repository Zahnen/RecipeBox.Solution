using System.Collections.Generic;

namespace RecipeBox.Models
{
  public class Tag
  {
    public Tag()
    {
      this.Recipes = new HashSet<Recipe>();
    }

    public string Name {get; set;}
    public virtual int ApplicationUser {get; set;}
    public virtual ICollection<RecipeTag> Recipes {get; set;}
  }
}