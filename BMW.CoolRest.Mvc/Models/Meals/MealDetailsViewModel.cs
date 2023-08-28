using BMW.CoolRest.Mvc.Models.Ingredients;

namespace BMW.CoolRest.Mvc.Models.Meals
{
    public class MealDetailsViewModel
    {
        public MealDetailsViewModel()
        {
            Ingredients = new List<IngredientViewModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public List<IngredientViewModel> Ingredients { get; set; }
    }
}
