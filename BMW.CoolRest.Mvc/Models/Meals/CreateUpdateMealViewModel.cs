using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BMW.CoolRest.Mvc.Models.Meals
{
    public class CreateUpdateMealViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }


        [Display(Name = "Ingredients")]
        public List<int> IngredientIds { get; set; }
        
        [ValidateNever]
        public MultiSelectList IngredientsMultiselectList { get; set; }
    }
}
